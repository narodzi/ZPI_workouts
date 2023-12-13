import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Cat } from '../models/cat.model';
import { ModifyCatComponent } from '../modify-cat/modify-cat.component';

@Component({
  selector: 'app-cats-component',
  standalone: true,
  imports: [
    CommonModule,
    ModifyCatComponent
  ],
  templateUrl: './cats-component.component.html',
  styleUrl: './cats-component.component.scss',
  changeDetection: ChangeDetectionStrategy.Default,
})
export class CatsComponentComponent { 
  cats$: Observable<Cat[]>

  editedCat: Cat | undefined

  formVisible = false

  constructor(private readonly http: HttpClient) {
    this.cats$ = http.get<Cat[]>(`http://localhost:5125/cats`)
  }

  deleteCat(id: number) {
    this.http.delete<number>(`http://localhost:5125/cats/${id}`).subscribe({
      next: () => this.cats$ = this.http.get<Cat[]>(`http://localhost:5125/cats`)
    })
  }

  addCat() {
    this.editedCat = undefined
    this.formVisible = true
  }

  closeForm() {
    this.editedCat = undefined
    this.formVisible = false
  }

  editCat(cat: Cat) {
    this.editedCat = undefined
    this.editedCat = cat
    this.formVisible = true
  }

  handleSaveCat(cat: Cat) {
    if(cat.id) {
      this.http.put<number>(`http://localhost:5125/cats`, cat).subscribe({
        next: () => this.cats$ = this.http.get<Cat[]>(`http://localhost:5125/cats`)
      })
    }
    else {
      this.http.post<number>(`http://localhost:5125/cats`, cat).subscribe({
        next: () => this.cats$ = this.http.get<Cat[]>(`http://localhost:5125/cats`)
      })
    }
    this.closeForm()
  }
}
