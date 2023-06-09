import AddCountry from "./components/AddCountry";
import CountryList from "./components/CountryList";
import AddCountryRequest from "./dtos/requests/AddCountryRequest";

function App() {
  return (
    <div className="App">
      <AddCountry
        addCountry={function (country: AddCountryRequest): void {
          throw new Error("Function not implemented.");
        }}
      />
      <CountryList />
    </div>
  );
}

export default App;
