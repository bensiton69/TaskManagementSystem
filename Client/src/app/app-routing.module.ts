import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { HomeComponent } from './Components/home/home.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    children: [
      // { path: 'Task/New', component: TaskFormComponent },
      // { path: 'Vehicles/:id', component: TaskFormComponent },
      // { path: 'Vehicles', component: TaskListComponent },
    ]
  },

  { path: '**', component: AppComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
