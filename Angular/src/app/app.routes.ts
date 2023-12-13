import { Routes } from '@angular/router';
import { CatsComponentComponent } from './cats-component/cats-component.component';

export const routes: Routes = [
    {
        path: "**",
        component: CatsComponentComponent
    }
];
