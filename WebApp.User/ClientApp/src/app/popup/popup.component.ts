import { Component, OnInit, AfterViewInit } from '@angular/core';
import { ChatbotService } from '../services/chatbot.service';

declare const $: any;

@Component({
  selector: 'app-popup',
  templateUrl: './popup.component.html',
  styleUrls: ['./popup.component.css']
})
export class PopupComponent implements AfterViewInit {

  constructor(
    private chatbotService: ChatbotService
  ) { }
  str: string;
  arr = [
    // { key: true, value: 'awesadfdsfdsfadsfbc' },
    // { key: true, value: 'adsfadsfd' },
    // { key: false, value: 'aqwsafdsfdsfdsec' },
    // { key: false, value: 'asafdsfdsfdsfdbc' },
    // { key: true, value: 'abwqfdsfdsfdsfffdsafdsfdsec' },
    // { key: false, value: 'abadsafdsfdsfdsfdssdc' },
    // { key: true, value: 'adsfadsfd' },
    // { key: false, value: 'aqwsafdsfdsfdsec' },
    // { key: false, value: 'asafdsfdsfdsfdbc' },
    // { key: true, value: 'abwqfdsfdsfdsfffdsafdsfdsec' },
    // { key: false, value: 'abadsafdsfdsfdsfdssdc' },
  ];
  ngAfterViewInit() {
    const arrow = $('.chat-head img');
    const textarea = $('.chat-text textarea');

    arrow.on('click', function () {
      const src = arrow.attr('src');

      $('.chat-body').slideToggle('fast');
      if (src === 'https://maxcdn.icons8.com/windows10/PNG/16/Arrows/angle_down-16.png') {
        arrow.attr('src', 'https://maxcdn.icons8.com/windows10/PNG/16/Arrows/angle_up-16.png');
      } else {
        arrow.attr('src', 'https://maxcdn.icons8.com/windows10/PNG/16/Arrows/angle_down-16.png');
      }
    });

    textarea.keypress(function (event) {
      const $this = $(this);
      if (event.keyCode === 13) {
        const msg = $this.val();
        $this.val('');
        $('.chat-body').stop().animate({ scrollTop: $('.chat-body')[0].scrollHeight }, 1000);
      }
    });
  }

  onKeypressEvent(value) {
    if (value.code === 'Enter') {
      this.arr.push({ key: true, value: this.str });
      let data = { data: this.str };
      this.chatbotService.createChatbot(data).subscribe(data1 => {
        this.arr.push({ key: false, value: data1['response'] });
      });
    }
  }

}
