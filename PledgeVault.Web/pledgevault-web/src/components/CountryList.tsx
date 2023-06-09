import React, { useEffect, useState } from "react";
import { Country } from "../models/Country";
import { Page } from "../models/Page";

const CountryList: React.FC = () => {
  const [countries, setCountries] = useState<Page<Country>>({
    pageNumber: 0,
    pageSize: 0,
    totalItems: 0,
    totalPages: 0,
    data: [],
  });

  useEffect(() => {
    fetch("https://localhost:7285/api/countries")
      .then((response) => {
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        return response.json();
      })
      .then((data) => setCountries(data))
      .catch((error) => {
        console.error(
          "There has been a problem with the fetch operation:",
          error
        );
      });
  }, []);

  return (
    <div className="p-6">
      <h1 className="text-4xl font-bold mb-6">Countries</h1>
      {countries.data.map((country) => (
        <div
          key={country.id}
          className="mb-6 p-4 border-2 border-blue-200 rounded-md"
        >
          <h2 className="text-2xl font-semibold mb-2">{country.name}</h2>
          <p className="mb-4">{country.summary}</p>
          {country.parties && country.parties.length > 0 && (
            <div>
              <h3 className="text-lg font-semibold mb-2">Parties</h3>
              {country.parties.map((party) => (
                <div key={party.id} className="mb-2 pl-2">
                  <div className="flex items-center">
                    <h4 className="text-base font-semibold">{party.name}</h4>
                    {party.dateEstablished && (
                      <p className="text-xs ml-2">
                        [Est. {new Date(party.dateEstablished).getFullYear()}]
                      </p>
                    )}
                  </div>
                  <p className="text-sm">{party.summary}</p>
                </div>
              ))}
            </div>
          )}
        </div>
      ))}
    </div>
  );
};

export default CountryList;
