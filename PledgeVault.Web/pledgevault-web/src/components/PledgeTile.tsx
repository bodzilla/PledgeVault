import React, { useState } from "react";
import { ReactComponent as ThumbsUp } from "../assets/thumbs-up.svg";
import { ReactComponent as ThumbsDown } from "../assets/thumbs-down.svg";
import { Pledge } from "../models/Pledge";

interface PledgeTileProps {
  pledge: Pledge;
}

const PledgeTile: React.FC<PledgeTileProps> = ({ pledge }) => {
  const [isHovered, setIsHovered] = useState(false);
  const [voteCount, setVoteCount] = useState(pledge.score);
  const [userVote, setUserVote] = useState(0); // -1 for downvote, 1 for upvote, 0 for no vote

  const handleUpvote = () => {
    if (userVote === 1) {
      // user wants to undo their upvote
      setUserVote(0);
      setVoteCount(voteCount - 1);
    } else {
      // If user had downvoted before, increment the count by 2 (1 for undoing downvote, 1 for the upvote)
      // Otherwise just increment by 1 for the upvote
      setVoteCount(voteCount + (userVote === -1 ? 2 : 1));
      setUserVote(1);
    }
  };

  const handleDownvote = () => {
    if (userVote === -1) {
      // user wants to undo their downvote
      setUserVote(0);
      setVoteCount(voteCount + 1);
    } else {
      // If user had upvoted before, decrement the count by 2 (1 for undoing upvote, 1 for the downvote)
      // Otherwise just decrement by 1 for the downvote
      setVoteCount(voteCount - (userVote === 1 ? 2 : 1));
      setUserVote(-1);
    }
  };

  return (
    <div
      className="p-6 max-w-lg flex items-stretch"
      onMouseEnter={() => setIsHovered(true)}
      onMouseLeave={() => setIsHovered(false)}
    >
      <div className="flex flex-col items-center justify-around mr-4">
        <button
          className={`p-1 rounded ${
            userVote === 1 ? "bg-lime-200" : "hover:bg-lime-100"
          } ${isHovered || userVote === 1 ? "" : "opacity-0"}`}
          onClick={handleUpvote}
        >
          <ThumbsUp className="h-6 w-6" stroke="darkgreen" />
        </button>
        <span className="text-2xl font-semibold">{voteCount}</span>
        <button
          className={`p-1 rounded ${
            userVote === -1 ? "bg-rose-200" : "hover:bg-rose-100"
          } ${isHovered || userVote === -1 ? "" : "opacity-0"}`}
          onClick={handleDownvote}
        >
          <ThumbsDown className="h-6 w-6" stroke="darkred" />
        </button>
      </div>
      <div className="p-6 rounded-md shadow flex-1">
        <h2 className="text-2xl font-semibold mb-2">
          {pledge.politician?.name}
        </h2>
        <p className="mb-4">{pledge.title}</p>
      </div>
    </div>
  );
};

export default PledgeTile;
