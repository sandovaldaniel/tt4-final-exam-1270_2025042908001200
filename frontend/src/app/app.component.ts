// src/app/app.component.ts
import { Component, OnInit } from '@angular/core';
import { ExpenseService } from './services/expense.service';
import { Expense } from './models/expense';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false
})
export class AppComponent implements OnInit {
  title = 'frontend';  
  expenses: Expense[] = [];
  form: FormGroup;

  constructor(private expenseService: ExpenseService, private fb: FormBuilder) {
    this.form = fb.group({
      title: ['', [Validators.required]],
      category: ['', [Validators.required]],
      amount: [null, [Validators.required]],
      date: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.loadExpenses();
  }

  loadExpenses() {
    this.expenseService.getExpenses().subscribe((data) => (this.expenses = data));
  }

  createExpense() {
    if (this.form.invalid) return;

    const { title, category, amount, date } = this.form.value;

    const expense: Expense = {
      title,
      category,
      amount,
      date
    };

    this.expenseService.createExpense(expense).subscribe(() => {
      this.form.reset();
      this.form.markAsPristine();
      this.form.markAsUntouched();
      this.loadExpenses();
    });
  }

  deleteExpense(id: number) {
    this.expenseService.deleteExpense(id).subscribe(() => this.loadExpenses());
  }
}

