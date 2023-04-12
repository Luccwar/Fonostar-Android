using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReceiveResult : MonoBehaviour {
    //public Text   wordText;
   
    public TextMeshProUGUI messageText;
    

    private int remainig;
    private string wordRecognized;    

    private void Start() {
        //Pause the game
        //TimeControl.PauseGame();
        
        //Set the listener to buttons
        this.gameObject.GetComponent<Button>().onClick.AddListener(StartRecordButtonOnClickHandler);
        //wordText.text = "Abacaxi";
        //Set the initial chances
        
    }

    public void SetWord(string word) {
        //wordText.text = word;
    }

    private void OnDestroy() {
        //TimeControl.ResumeGame();
    }

    private void StartRecordButtonOnClickHandler() {
        Debug.Log("Started record");
        //Task.BOX_TITLE = "Pronuncie: '" + wordText.text.ToUpper() + "'";
        Task.TaskOnClick();
    }

    /**
     *  This function receive the response of Native Speech Recognizer (Android)
     */
    void onActivityResult(string recognizedText) {
        char[] delimiterChars = { '~' };
        string[] result = recognizedText.Split(delimiterChars);

        //You can get the number of results with result.Length
        //And access a particular result with result[i] where i is an int
        //I have just assigned the best result to UI text
        //GameObject.Find("Text").GetComponent<Text>().text = result[0];
        wordRecognized = result[0].Split(' ')[0];

        checkWord();

        

    }

    private void checkWord() {
        if(wordRecognized != null) {
            messageText.text = "Acertou!";

            /*if ( wordText.text.Equals(wordRecognized, System.StringComparison.InvariantCultureIgnoreCase) ) {
                //Add score
                
                Destroy(this.gameObject);
            }
            else {
                Destroy(this.gameObject);
                
            }*/
        }
    }


}
