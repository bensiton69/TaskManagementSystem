import { TaskListComponent } from './Components/task-list/task-list.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { HomeComponent } from './Components/home/home.component';
import { TaskFormComponent } from './Components/task-form/task-form.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    children: [
       { path: 'Task/New', component: TaskFormComponent },
       { path: 'Task/:id', component: TaskFormComponent },
       { path: 'Tasks', component: TaskListComponent },
    ]
  },

  { path: '**', component: AppComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
