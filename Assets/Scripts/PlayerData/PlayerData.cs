using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


// TODO: this could maybe be a PlayerData? 
public class PlayerData : MonoBehaviour
{
    [SerializeField]
    private string[] mMoneyTierNames;

    [SerializeField]
    private string[] mInfluenceTierNames;

    [SerializeField]
    private float mIncrementalTickRate;

    [SerializeField]
    private bool mLogStateChanges = false;

    private float mTimeSinceTick; 

    private IncrementalContainer mMoneyIncremental;
    private IncrementalContainer mInfluenceIncremental;


    // TODO: real-estate needs a container that doesnt update

    // Start is called before the first frame update
    void Start()
    {
        mMoneyIncremental = new IncrementalContainer(mMoneyTierNames);
        mInfluenceIncremental = new IncrementalContainer(mInfluenceTierNames);
    }

    // Update is called once per frame
    void Update()
    {
        mTimeSinceTick += Time.deltaTime;

        bool changed = false;

        while(mTimeSinceTick > mIncrementalTickRate) {
            mTimeSinceTick -= mIncrementalTickRate;

            mMoneyIncremental.UpdateValues();
            mInfluenceIncremental.UpdateValues();

            changed = true;
        }

        if(changed && mLogStateChanges) {
            mMoneyIncremental.PrintValues();
            mInfluenceIncremental.PrintValues();
        }
    }
}
