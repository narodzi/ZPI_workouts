import { CommonModule, NgFor } from '@angular/common';
import { ChangeDetectionStrategy, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Cat } from '../models/cat.model';
import { FormBuilder, FormControl, NgForm, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-modify-cat',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './modify-cat.component.html',
  styleUrl: './modify-cat.component.scss',
  changeDetection: ChangeDetectionStrategy.Default,
})
export class ModifyCatComponent implements OnInit {
  _cat: Cat | undefined
  @Input() set cat (cat: Cat | undefined) {
    if(cat) {
      this.catForm.patchValue(cat)
    }
    this._cat = cat
  }
  get cat() {
    return this._cat
  }

  @Output() saveCat = new EventEmitter<Cat>()

  readonly catForm = this.fb.nonNullable.group({
    name: new FormControl<string | null>(null, Validators.required),
    race: new FormControl<string | null>(null, Validators.required),
    age: new FormControl<number | null>(null, Validators.required),
    color: new FormControl<string | null>(null, Validators.required),
  })

  constructor(private readonly fb: FormBuilder) {}

  ngOnInit() {
  }

  save() {
    if(this.catForm.valid) {
      if(this.cat) {
        this.saveCat.emit({
          ...this.cat,
          name: this.catForm.controls.name.value,
          race: this.catForm.controls.race.value,
          age: this.catForm.controls.age.value,
          color: this.catForm.controls.color.value
        })
      }
      else {
        this.saveCat.emit({
          name: this.catForm.controls.name.value,
          race: this.catForm.controls.race.value,
          age: this.catForm.controls.age.value,
          color: this.catForm.controls.color.value
        })
      }
    }
  }
}
