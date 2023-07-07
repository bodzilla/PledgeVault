import React, { useState } from "react";
import { ReactComponent as ThumbsUp } from "../assets/thumbs-up.svg";
import { ReactComponent as ThumbsDown } from "../assets/thumbs-down.svg";
import { ReactComponent as ThumbsUpHover } from "../assets/thumbs-up-hover.svg";
import { ReactComponent as ThumbsDownHover } from "../assets/thumbs-down-hover.svg";
import { ReactComponent as ThumbsUpFilled } from "../assets/thumbs-up-filled.svg";
import { ReactComponent as ThumbsDownFilled } from "../assets/thumbs-down-filled.svg";
import { Pledge } from "../models/Pledge";

interface PledgeTileProps {
  pledge: Pledge;
}

const PledgeTile: React.FC<PledgeTileProps> = ({ pledge }) => {
  const [voteCount, setVoteCount] = useState(pledge.score);
  const [hoverUp, setHoverUp] = useState(false);
  const [hoverDown, setHoverDown] = useState(false);
  const [voteUp, setVoteUp] = useState(false);
  const [voteDown, setVoteDown] = useState(false);
  const [userVote, setUserVote] = useState(0); // -1 for downvote, 1 for upvote, 0 for no vote

  const handleUpvote = () => {
    if (userVote === 1) {
      // user wants to undo their upvote
      setUserVote(0);
      setVoteCount(voteCount - 1);
      setVoteUp(false);
    } else {
      // If user had downvoted before, increment the count by 2 (1 for undoing downvote, 1 for the upvote)
      // Otherwise just increment by 1 for the upvote
      setVoteCount(voteCount + (userVote === -1 ? 2 : 1));
      setUserVote(1);
      setVoteUp(true);
    }
    setVoteDown(false);
  };

  const handleDownvote = () => {
    if (userVote === -1) {
      // user wants to undo their downvote
      setUserVote(0);
      setVoteCount(voteCount + 1);
      setVoteDown(false);
    } else {
      // If user had upvoted before, decrement the count by 2 (1 for undoing upvote, 1 for the downvote)
      // Otherwise just decrement by 1 for the downvote
      setVoteCount(voteCount - (userVote === 1 ? 2 : 1));
      setUserVote(-1);
      setVoteDown(true);
    }
    setVoteUp(false);
  };

  return (
    <div className="p-6 max-w-lg flex items-stretch">
      <div className="flex flex-col items-center justify-around mr-4">
        <button
          className={`p-1 rounded`}
          onClick={handleUpvote}
          onMouseEnter={() => setHoverUp(true)}
          onMouseLeave={() => setHoverUp(false)}
        >
          {voteUp ? (
            <ThumbsUpFilled className="h-6 w-6" />
          ) : hoverUp ? (
            <ThumbsUpHover className="h-6 w-6" />
          ) : (
            <ThumbsUp className="h-6 w-6" />
          )}
        </button>
        <span className="text-2xl font-semibold">{voteCount}</span>
        <button
          className={`p-1 rounded`}
          onClick={handleDownvote}
          onMouseEnter={() => setHoverDown(true)}
          onMouseLeave={() => setHoverDown(false)}
        >
          {voteDown ? (
            <ThumbsDownFilled className="h-6 w-6" />
          ) : hoverDown ? (
            <ThumbsDownHover className="h-6 w-6" />
          ) : (
            <ThumbsDown className="h-6 w-6" />
          )}
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
