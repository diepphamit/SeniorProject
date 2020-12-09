import { Component, OnInit, AfterViewInit } from '@angular/core';
import { CURRENT_USER } from 'src/app/constants/db-keys';
import { ChatbotService } from 'src/app/services/chatbot.service';
import { FlashcardForCreateByChatbot } from './Dto/flashcardForCreateByChatbot.model';

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
    //{ key: 2, value: 'http://res.cloudinary.com/djmeq19id/image/upload/v1606580905/s9clzx7fwa6alf0qpxki.jpg' },
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
  }

  onKeypressEvent(value) {
    if (value.code === 'Enter' && this.str.trim() !== '') {
      this.arr.push({ key: 0, value: this.str });
      const dataRequest = { data: this.str };
      this.str = '';
      if (dataRequest.data.includes('make a flashcard') || dataRequest.data.includes('make flashcard') || dataRequest.data.includes('create a flashcard') || dataRequest.data.includes('create flashcard')) {

        const flashcard = new FlashcardForCreateByChatbot(dataRequest.data.split('"', 2)[1], Number(this.getuserId));
        this.chatbotService.createFlashcardByChatbot(flashcard).subscribe(data => {
          this.arr.push({ key: 1, value: 'Made successfully, You can check in "My flashcard"' });
          $('.chat-body').animate({ scrollTop: $('.chat-body')[0].scrollHeight }, 500);
        });
      } else {
        this.chatbotService.createChatbot(dataRequest).subscribe(dataResponse => {
          if (dataResponse['response'] === null) {
            this.arr.push({ key: 1, value: 'Sorry! I cannot understand what you say!' });
          } else if (dataResponse['response'].includes('http')) {
            this.arr.push({ key: 2, value: dataResponse['response'] });
          } else {
            this.arr.push({ key: 1, value: dataResponse['response'] });
          }
          $('.chat-body').animate({ scrollTop: $('.chat-body')[0].scrollHeight }, 500);
        });
      }
    }

  }
  send() {
    if (this.str.trim() !== '') {
      this.arr.push({ key: 0, value: this.str });
      const dataRequest = { data: this.str };
      this.str = '';
      if (dataRequest.data.includes('make a flashcard') || dataRequest.data.includes('make flashcard') || dataRequest.data.includes('create a flashcard') || dataRequest.data.includes('create flashcard')) {

        const flashcard = new FlashcardForCreateByChatbot(dataRequest.data.split('"', 2)[1], Number(this.getuserId));
        this.chatbotService.createFlashcardByChatbot(flashcard).subscribe(data => {
          this.arr.push({ key: 1, value: 'Made successfully, You can check in "My flashcard"' });
          $('.chat-body').animate({ scrollTop: $('.chat-body')[0].scrollHeight }, 500);
        });
      } else {
        this.chatbotService.createChatbot(dataRequest).subscribe(dataResponse => {
          if (dataResponse['response'] === null) {
            this.arr.push({ key: 1, value: 'Sorry! I cannot understand what you say!' });
          } else if (dataResponse['response'].includes('http')) {
            this.arr.push({ key: 2, value: dataResponse['response'] });
          } else {
            this.arr.push({ key: 1, value: dataResponse['response'] });
          }
          $('.chat-body').animate({ scrollTop: $('.chat-body')[0].scrollHeight }, 500);
        });
      }


    }

  }

  get getuserId() {
    const user = JSON.parse(localStorage.getItem(CURRENT_USER));

    return user.id;
  }

}
