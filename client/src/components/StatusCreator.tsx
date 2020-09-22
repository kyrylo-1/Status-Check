import React from 'react';

export const StatusCreator = () => {
  return (
    <div className="container mx-auto">
      <span>What did you do yesterday?</span>
      <input
        className="bg-white focus:outline-none focus:shadow-outline
          border border-gray-300 rounded-lg py-1 px-2 h-16 block w-50 appearance-none leading-normal"
        type="text"
      ></input>
      <button className="btn btn-blue mt-3">Button</button>
    </div>
  );
};
