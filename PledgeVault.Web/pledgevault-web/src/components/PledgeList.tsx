import React, { useEffect, useState } from "react";
import PledgeTile from "./PledgeTile";
import { Pledge } from "../models/Pledge";

const PledgeList: React.FC = () => {
  const [pledge, setPledge] = useState<Pledge | null>();

  useEffect(() => {
    fetch("https://localhost:7285/api/pledges/id/1")
      .then((response) => response.json())
      .then((data) => setPledge(data));
  }, []);

  return <div>{pledge && <PledgeTile pledge={pledge} />}</div>;
};

export default PledgeList;
