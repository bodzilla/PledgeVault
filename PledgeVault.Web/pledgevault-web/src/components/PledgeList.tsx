import React, { useEffect, useState } from "react";
import PledgeTile from "./PledgeTile";
import { Pledge } from "../models/Pledge";
import { Page } from "../models/Page";

const PledgeList: React.FC = () => {
  const [pledges, setPledges] = useState<Page<Pledge>>({
    pageNumber: 0,
    pageSize: 0,
    totalItems: 0,
    totalPages: 0,
    data: [],
  });

  useEffect(() => {
    fetch("https://localhost:7285/api/pledges")
      .then((response) => {
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        return response.json();
      })
      .then((data) => setPledges(data))
      .catch((error) => {
        console.error(
          "There has been a problem with the fetch operation:",
          error
        );
      });
  }, []);

  return (
    <>
      {pledges.data.map((pledge) => (
        <PledgeTile pledge={pledge} key={pledge.id} />
      ))}
    </>
  );
};

export default PledgeList;
