import React, { useState } from "react";
import GovernmentType from "../enums/GovernmentType";
import AddCountryRequest from "../dtos/requests/AddCountryRequest";

interface AddCountryProps {
    addCountry: (country: AddCountryRequest) => void;
}

const AddCountry: React.FC<AddCountryProps> = ({ addCountry }) => {
    const [newCountryRequest, setNewCountryRequest] = useState<AddCountryRequest>(
        {
            name: "",
            dateEstablished: new Date(),
            governmentType: GovernmentType.Democracy,
            summary: null,
        }
    );

    const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        fetch("https://localhost:7285/api/countries", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(newCountryRequest),
        })
            .then((response) => response.json())
            .then((data) => {
                addCountry(data);
                setNewCountryRequest({
                    name: "",
                    dateEstablished: null,
                    governmentType: GovernmentType.Democracy,
                    summary: null,
                });
            })
            .catch((error) => console.error(error));
    };

    const handleInputChange = (
        e: React.ChangeEvent<
            HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement
        >
    ) => {
        setNewCountryRequest({
            ...newCountryRequest,
            [e.target.name]: e.target.value,
        });
    };

    return (
        <div className="flex justify-center">
            <form className="w-full max-w-lg" onSubmit={handleSubmit}>
                <div className="flex flex-wrap -mx-3 mb-6">
                    <div className="w-full md:w-1/2 px-3 mb-6 md:mb-0">
                        <label
                            className="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
                            htmlFor="name"
                        >
                            Country Name
                        </label>
                        <input
                            className="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white"
                            id="name"
                            type="text"
                            name="name"
                            value={newCountryRequest.name}
                            onChange={handleInputChange}
                            required
                        />
                    </div>
                    <div className="w-full md:w-1/2 px-3">
                        <label
                            className="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
                            htmlFor="dateEstablished"
                        >
                            Date Established
                        </label>
                        <input
                            className="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white"
                            id="dateEstablished"
                            type="date"
                            name="dateEstablished"
                            value={
                                newCountryRequest.dateEstablished
                                    ? newCountryRequest.dateEstablished
                                        .toISOString()
                                        .substring(0, 10)
                                    : ""
                            }
                            onChange={handleInputChange}
                            required
                        />
                    </div>
                </div>
                <div className="flex flex-wrap -mx-3 mb-2">
                    <div className="w-full md:w-1/2 px-3 mb-6 md:mb-0">
                        <label
                            className="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
                            htmlFor="governmentType"
                        >
                            Government Type
                        </label>
                        <div className="relative">
                            <select
                                className="block appearance-none w-full bg-gray-200 border border-gray-200 text-gray-700 py-3 px-4 pr-8 rounded leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
                                id="governmentType"
                                name="governmentType"
                                value={newCountryRequest.governmentType}
                                onChange={handleInputChange}
                                required
                            >
                                {Object.keys(GovernmentType).map((type, index) => (
                                    <option
                                        key={index}
                                        value={GovernmentType[type as keyof typeof GovernmentType]}
                                    >
                                        {type}
                                    </option>
                                ))}
                            </select>
                            <div className="pointer-events-none absolute inset-y-0 right-0 flex items-center px-2 text-gray-700">
                                <svg
                                    className="fill-current h-4 w-4"
                                    xmlns="http://www.w3.org/2000/svg"
                                    viewBox="0 0 20 20"
                                >
                                    <path d="M0 6l10 10 10-10z" />
                                </svg>
                            </div>
                        </div>
                    </div>
                    <div className="w-full md:w-1/2 px-3">
                        <label
                            className="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
                            htmlFor="summary"
                        >
                            Summary
                        </label>
                        <input
                            className="appearance-none block w-full bg-gray-200 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white"
                            id="summary"
                            type="text"
                            name="summary"
                            value={newCountryRequest.summary || ""}
                            onChange={handleInputChange}
                        />
                    </div>
                </div>
                <button
                    className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
                    type="submit"
                >
                    Add Country
                </button>
            </form>
        </div>
    );
};

export default AddCountry;
