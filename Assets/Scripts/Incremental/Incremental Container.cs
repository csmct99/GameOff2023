using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: scriptable object? dont really need anything
public class IncrementalContainer {

    // TODO: float is probably big enough, its 38 digits, but could make a custom type 
    private float[] mTierCounts;
    private float[] mTierMultiplier;
    private string[] mTierNames; 
    private float mCurrency;

    private const bool CREATE_LOWER = true; // false = make money at each tier, true = create lower tier 
    private const float INITIAL_COUNT = 1;
    private const float INITIAL_MULTIPLIER = 1;

    public IncrementalContainer(string[] tierNames) {
        mCurrency = 0;
        
        mTierNames = tierNames;
        mTierCounts = new float[mTierNames.Length];
        mTierMultiplier = new float[mTierNames.Length];

        for(int i = 0; i < mTierNames.Length; ++i) {
            mTierCounts[i] = INITIAL_COUNT;
            mTierMultiplier[i] = INITIAL_MULTIPLIER;
        }

        mTierCounts[0] = 1; // start with 1 lowest tier to build from
    }

    public void UpdateValues()
    {
        if(CREATE_LOWER) {
            // everything makes 1 tier below it

            mCurrency += mTierMultiplier[0] * mTierCounts[0];
            for(int i = 0; i < mTierCounts.Length - 1; ++i) {
                mTierCounts[i] += mTierCounts[i + 1] * mTierMultiplier[i + 1];
            }
        } else {
            // everything makes money

            for(int i = 0; i < mTierCounts.Length; ++i) {
                mCurrency += mTierCounts[i] * mTierMultiplier[i];
            }
        }
    }

    public void PrintValues() {
        string lineString = "Currency: " + mCurrency + ", ";
        for(int i = 0; i < mTierNames.Length; ++i) {
            lineString += mTierNames[i] + ": " + mTierCounts[i] + ", ";
        }
        Debug.Log(lineString);
    }
}
