import React, { useState, useEffect, ChangeEvent } from 'react';
import axios from 'axios';
import './App.css';
import { CompanyData } from './types';

const App: React.FC = () => {

  const [currentYear] = useState<number>(new Date().getFullYear());
  const [availableYears, setAvailableYears] = useState<number[]>([]);
  const [year, setYear] = useState<number>(currentYear);
  const [data, setData] = useState<CompanyData[]>([]);
  const [loading, setLoading] = useState<boolean>(false);


  useEffect(() => {
    fetchYears();
  }, []);

  useEffect(() => {
    fetchData();
  }, [year]);


  const fetchYears = async () => {
    setLoading(true);
    try {
      const response = await axios.get<number[]>(`https://localhost:7075/lookup/available-years`);
      setAvailableYears(response.data);
    } catch (error) {
      console.error('Error fetching data', error);
    } finally {
      setLoading(false);
    }
  };

  const fetchData = async () => {
    setLoading(true);
    try {
      const response = await axios.get<CompanyData[]>(`https://localhost:7075/contracts/${year}`);
      setData(response.data);
    } catch (error) {
      console.error('Error fetching data', error);
    } finally {
      setLoading(false);
    }
  };

  const handleYearChange = (event: ChangeEvent<HTMLSelectElement>) => {
    setYear(Number(event.target.value));
  };

  const months = [
    'January', 'February', 'March', 'April', 'May', 'June',
    'July', 'August', 'September', 'October', 'November', 'December'
  ];

  return (
    <div className="App">
      <h1>Company Prices</h1>
      <div className="year-selector">
        <label htmlFor="year">Select Year: </label>
        <select id="year" value={year} onChange={handleYearChange}>
          {availableYears.map(y => (
            <option key={y} value={y}>{y}</option>
          ))}
        </select>
      </div>
      {loading ? (
        <p>Loading...</p>
      ) : (
        <table>
          <thead>
          <tr>
            <th>Company</th>
            <th>Contract</th>
            {months.map(month => (
              <th key={month}>{month}</th>
            ))}
          </tr>
          </thead>
          <tbody>
          {data.map(company => (
            <tr key={company.companyId}>
              <td>{company.companyName}</td>
              <td>{company.contractName}</td>
              {months.map((month, index) => {
                const prices = company.hourlyPrices.filter(p => p.month === index + 1);
                return (
                  <td key={index}>
                    {prices.map((p, i) => (
                      <React.Fragment key={i}>
                        <span title={'started on: ' + p.startDateAgreement.toString()}>{p.pricePerHour}</span>
                        {i < prices.length - 1 && ' / '}
                      </React.Fragment>
                    ))}
                  </td>
                );
              })}
            </tr>
          ))}
          </tbody>
        </table>
      )}
    </div>
  );
};

export default App;
