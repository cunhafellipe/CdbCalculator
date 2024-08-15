import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { CdbCalculationComponent } from './cdb-calculation.component';
import { FormsModule } from '@angular/forms';
import { CdbCalculationResult } from './cdb-calculation-result.model';

describe('CdbCalculationComponent', () => {
  let component: CdbCalculationComponent;
  let fixture: ComponentFixture<CdbCalculationComponent>;
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, FormsModule, CdbCalculationComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CdbCalculationComponent);
    component = fixture.componentInstance;
    httpMock = TestBed.inject(HttpTestingController);
    fixture.detectChanges();
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display the correct title', () => {
    const h2Element = fixture.nativeElement.querySelector('h2');
    expect(h2Element.textContent).toBe('Cálculo CDB');
  });


  it('should initialize with default values', () => {
    expect(component.initialValue).toBe(0);
    expect(component.months).toBe(2);
    expect(component.result).toBeUndefined();
  });

  it('should have form inputs for initial value and months', () => {
    const initialValueInput = fixture.nativeElement.querySelector('#initialValue');
    const monthsInput = fixture.nativeElement.querySelector('#months');
    expect(initialValueInput).toBeTruthy();
    expect(monthsInput).toBeTruthy();
  });

  it('should call the API and update result on calculateCdb()', () => {
    const mockResult: CdbCalculationResult = {
      grossValue: 1100,
      netValue: 1080,
      monthlyValues: [1000, 1050, 1100]
    };

    component.initialValue = 1000;
    component.months = 3;
    component.calculateCdb();

    const req = httpMock.expectOne('http://localhost:5240/api/CdbCalculation');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual({ InitialValue: 1000, Months: 3 });

    req.flush(mockResult);

    expect(component.result).toEqual(mockResult);
  });

  it('should log an error when API call fails', () => {
    spyOn(console, 'error');

    component.calculateCdb();

    const req = httpMock.expectOne('http://localhost:5240/api/CdbCalculation');
    req.error(new ProgressEvent('error'), {
      status: 404,
      statusText: 'Not Found',
    });

    expect(console.error).toHaveBeenCalledWith('Error calculating CDB:', jasmine.any(Object));
  });

  it('should display results when calculation is successful', () => {
    const mockResult: CdbCalculationResult = {
      grossValue: 1100,
      netValue: 1080,
      monthlyValues: [1000, 1050, 1100]
    };

    component.result = mockResult;
    fixture.detectChanges();

    const resultElements = fixture.nativeElement.querySelectorAll('h3');
    expect(resultElements[0].textContent).toBe('Resultados:');
    expect(resultElements[1].textContent).toBe('Detalhamento:');

    const paragraphs = fixture.nativeElement.querySelectorAll('p');
    expect(paragraphs[0].textContent).toContain('Valor Bruto:');
    expect(paragraphs[1].textContent).toContain('Valor Líquido:');
  });
});
