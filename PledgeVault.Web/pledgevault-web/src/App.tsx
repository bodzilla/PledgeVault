import AddCountry from "./components/AddCountry";
import CountryList from "./components/CountryList";
import PledgeList from "./components/PledgeList";
import PledgeTile from "./components/PledgeTile";
import AddCountryRequest from "./dtos/requests/AddCountryRequest";

function App() {
  return (
    <div className="App">
      <PledgeList />
      {/* <PledgeTile />
      <PledgeTile /> */}
      {/* <AddCountry
        addCountry={function (country: AddCountryRequest): void {
          throw new Error("Function not implemented.");
        }}
      /> */}
      <CountryList />
    </div>
  );
}

export default App;
