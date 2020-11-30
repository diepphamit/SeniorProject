import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddFavoriteFlashcardComponent } from './add-favorite-flashcard.component';

describe('AddFavoriteFlashcardComponent', () => {
  let component: AddFavoriteFlashcardComponent;
  let fixture: ComponentFixture<AddFavoriteFlashcardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddFavoriteFlashcardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddFavoriteFlashcardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
