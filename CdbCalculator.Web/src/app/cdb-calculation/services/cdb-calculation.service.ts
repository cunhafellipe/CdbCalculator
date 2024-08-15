import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { CdbCalculationResult } from '../models/cdb-calculation-result.model';

@Injectable({
  providedIn: 'root'
})
export class CdbCalculationService {

  private apiUrl = 'http://localhost:5240/api/CdbCalculation';

  constructor(private http: HttpClient) { }

  calculateCdb(initialValue: number, months: number): Observable<CdbCalculationResult> {
    return this.http.post<CdbCalculationResult>(this.apiUrl, { InitialValue: initialValue, Months: months })
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'Erro desconhecido!';
    if (error.error instanceof ErrorEvent) {
      errorMessage = `Ocorreu um erro: ${error.error.message}`;
    } else if (error.status === 0) {
      errorMessage = 'Não foi possível conectar ao servidor. Por favor, verifique sua conexão e tente novamente.';
    } else if (error.error?.errors) {
      const errorMessages = Object.values(error.error.errors).flat();
      errorMessage = errorMessages.join(' ');
    } else if (typeof error.error === 'string') {
      errorMessage = error.error;
    } else {
      errorMessage = `Erro ${error.status}: ${error.message}`;
    }
    return throwError(() => new Error(errorMessage));
  }
}
