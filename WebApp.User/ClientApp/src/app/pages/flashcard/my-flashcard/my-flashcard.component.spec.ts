import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MyFlashcardComponent } from './my-flashcard.component';

describe('MyFlashcardComponent', () => {
  let component: MyFlashcardComponent;
  let fixture: ComponentFixture<MyFlashcardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MyFlashcardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyFlashcardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
