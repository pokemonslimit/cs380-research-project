using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // ToList
using UnityEngine.UI;
using TMPro;


public class DisplayFishStats : MonoBehaviour
{
    public int index = 0;
    public TMP_Text utilityText;
    public TMP_Text behaviorText;
    public TMP_Text FishText; 
    public Button nextButton, previousButton;

    private List<string> keys;

    private agentAI agent;

    // Start is called before the first frame update
    void Start()
    {
        nextButton.onClick.AddListener(IncreaseIndex);
        previousButton.onClick.AddListener(DecrementIndex);
    }

    void IncreaseIndex()
    {
        index++;
    }
    void DecrementIndex()
    {
        index--;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit(); 


        if(keys != null)
        {
            if (index >= keys.Count)
                index = 0;
            else if (index < 0)
                index = keys.Count - 1;
        }
        
        // Because I always forget how this works 
        //https://docs.unity3d.com/ScriptReference/RaycastHit-point.html
        
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit objectHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out objectHit))
            {
                agent = objectHit.transform.GetComponent<agentAI>();

                if(agent != null)
                {
                    FishText.text = "Fish Selected: " + agent.transform.name;
                    keys = agent.utilityBlackboard.Keys.ToList();
                }
            }
        }
        if(keys != null && agent != null)
        {
            utilityText.text = "Blackboard Value: " + keys[index] + ": " + 
               string.Format("{0:0.#}", agent.utilityBlackboard[keys[index]]);

            behaviorText.text = "Tree Node: " + agent.currentNode.Name + "\n"
                + "Node's Utility Score: " 
                + string.Format("{0:0.#}", agent.currentNode.UtilityScore);
        }


    }
}
