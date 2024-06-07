export interface HourlyPrice {
  startDateAgreement: Date,
  pricePerHour: number;
  month: number;
}

export interface CompanyData {
  companyId: string;
  companyName: string;
  contractId: string;
  contractName: string;
  hourlyPrices: HourlyPrice[];
}
