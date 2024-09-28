using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DayManager : MonoBehaviour
{
    public static DayManager instance;
    public DayState currentDayState;
    
    public GameObject pagePrefab;
    public GameObject messagePrefab;
    
    public GameObject pagesHolder;
    public GameObject messageHolder;

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject cancelMessage;
    public GameObject cancelFolder;
    public GameObject folderButton;


    public GameObject desk;

    public Sprite sam;
    public Sprite politi;
    public Sprite col;
    public Sprite agent;
    public Sprite doc;
    public Sprite crack;
    public Sprite comp;

    

    public static bool mainIsLoading;
    public static bool isButtonReady;

    // Dictionary to hold the state transition maps for each day
    private Dictionary<int, Dictionary<int, int>> dayTransitionMap = new Dictionary<int, Dictionary<int, int>>();

    private Dictionary<int, List<(Sprite, string)>> dialogues = new Dictionary<int, List<(Sprite, string)>>();

    private Dictionary<int, string[]> dialogues1 = new Dictionary<int, string[]>
        {
            { 41, new [] {"-Bonjour Docteur, que pouvez-vous nous dire de l'état des hôpitaux actuellement ?","-Bonjour Sam, la situation est très tendue de notre côté. Nous sommes en sous-effectif et en souffrons grandement.", "-Qu'en est-il du moral des troupes ? ", "-Le personnel médical est épuisé mais nous faisons de notre mieux.","-Comment expliquer une telle situation ?","-Les urgences sont saturées dernièrement... *BRUIT DE FOND* excusez-moi il y a une urgence je dois vous laisser.","-Au revoir et bonne chance.", "-Merci. *BIP*" } },  // State 1 -> State 2
            { 42, new [] {"Hello","World" } }   // State 2 -> State 3
        };
    private Dictionary<int, string> debriefs = new Dictionary<int, string>
        {
            { 10, "vrai" },  // State 1 -> State 2
            { 11, "La nouvelle se répand que la Ricorée du Nord a effectué des essais de lancement militaires dans l'Océan Pacifish. Vous le saviez et vous n'avez rien fait, les lecteurs de votre journal sont déçus." },   // State 2 -> State 3
            { 31, "La construction de bunkers s'achève. Les forêts sont rasées, forçant des moustiques à se déplacer vers la ville. Vos lecteurs vous accusent d'avoir privilégié des intérêts privés." },   // State 2 -> State 3
            { 40, "Grâce à votre article, la population prend conscience de l'urgence de la situation. Pour éviter la catastrophe, la Cafédération met en place des mesures strictes que les citoyens suivent à la lettre. L'épidémie finit par être maîtrisée et le gouvernement salue votre implication dans la prévention de la crise. Votre journal gagne grandement en crédibilité."}
        };

    private Dictionary<int, string[]> folders = new Dictionary<int, string[]>
        {
            { 1, new [] {"La Ricorée du Nord se prépare à envoyer des missiles.\n\nInformations à disposition :\n\"Des sous-marins ##### nord ricoréens ont été aperçus dans l'Océan #####.\", Rapport des services de renseignement de la Cafédération\n\"J'ai intercepté des messages de bateaux nord ricoréens au large Los Anguille, sur la côte Pacifish.\", un contact au port de Los Anguille.","\n\"Une cargaison de missiles a quitté le pays récemment.\", Ingénieur militaire nord ricoréen."} },
            { 2, new [] {"\"Les usines tournent à plein régime, les machineries risquent de ne pas tenir.\", Syndicat des travailleurs\n\"Les ouvriers posent un préavis de grève, ils exigent une action du gouvernement.\", Syndicat des travailleurs\n\"Nos magasins sont en rupture de stock, les arrivages ne suffisent plus.\", Employé du supermarché Migral\n","\"Des supérettes ont été vandalisées la nuit dernière. Les dégâts s'élèvent à des milliers de CHF.\", Commandant de police\n\"La frontière avec la Fromance est engorgée, je n'ai jamais vu de tels embouteillages.\", Officier fédéral de douane.\n\"On manque de béton pour satisfaire les commandes des constructeurs de bunkers.\", Bétoprise (Entreprise de béton)"} },   // State 2 -> State 3
            { 3, new [] {"La hausse de construction de bunkers entraine une importante déforestation\n\nInformations à disposition :\"Nos bunkers respectent les normes environnementales.\", Entreprise de construction de bunkers\n\"Nos permis de construire nous ont été délivrés par la Cafédération.\", Entreprise de construction de bunkers\n\"La forêt de Broccoliande est dévastée. Ils abattent les arbres par centaines.\", Garde forestier\n\"","Le prix du bois est en chute libre, c'est le moment d'acheter pour préparer cet hiver.\", Employé d'un magasin de bricolage\n\"Notre terre est en danger, on lance une pétition pour la protéger.\", organisation écologique Beanpeace\n\"Nous donnerons bientôt une conférence de presse concernant notre projet en forêt.\", annonce de la Cafédération"} },   // State 2 -> State 3
            { 4, new [] {"Une crise sanitaire éclate dans la Cafédération\n\nInformations à disposition : \"Les services d'urgence des hopitaux sont encombrés.\", Extrait d'un communiqué du minsitère de la Santé.\n\"Le temps d'attente pour les appels d'urgence est supérieur à 2h.\", standardiste dans un CHU.\n\"Nous avons développé de nouveaux gants chirurgicaux sans latex.\", Entreprise de dispositifs médicaux\n\"","Mon équipe et moi-même avons découvert un nouveau virus.\", Professeur Tournedos, virologue\n\"Nous rédigeons de nouvelles directives sur la prévention des maladies pour préparer l'hiver à venir.\", Agent de l'OMS\n\"De nouvelles règles concernant la douane entreront en vigueur dès le mois prochain.\", Ministère de l'Intérieur"}},
            {5, new [] {"La Cafédération prévoit d'acquérir des avions de chasse\nInformations à disposition :\n\"La présentation de notre nouveau prototype est malheureusement repoussée à une date indéterminée.\", extrait d'une conférence de Dragairbus (entreprise d'aéronautique)\n\"Nos sites de construction tournent à 30% de leur capacité."," Ce ralentissement est dû à un manque d'acier majeur.\", communiqué de presse diffusé par Dragairbus\n\"Le gouvernement s'est réuni ce mardi pour discuter des budgets\", Porte-parole du gouvernement\n\"J'ai entendu des gradés dire que le service militaire allait être raccourci.\""," Source anonyme\n\"L'action de Dragairbus a perdu 11% en une semaine, j'ai peur pour mes dividendes.\", Actionnaire de Dragairbus\n\"Vu la situation actuelle, il faut se préparer à des coupes budgétaires du gouvernement.\", Economiste"}}
        };

    private void Start()
    {
        dialogues.Add(41, new List<(Sprite, string)>{(sam,"Bonjour Docteur, que pouvez-vous nous dire de l'état des hôpitaux actuellement ?"),(doc,"-Bonjour Sam, la situation est très tendue de notre côté. Nous sommes en sous-effectif et en souffrons grandement."),(sam,"-Qu'en est-il du moral des troupes ? "),(doc,"-Le personnel médical est épuisé mais nous faisons de notre mieux."), (sam,"-Comment expliquer une telle situation ?"),(doc,"-Les urgences sont saturées dernièrement... *BRUIT DE FOND* excusez-moi il y a une urgence je dois vous laisser."),(sam,"-Au revoir et bonne chance."),(doc,"-Merci. *BIP*")});
        dialogues.Add(42, new List<(Sprite, string)>{(sam,"Bonjour, merci d'accepter de discuter avec moi."),(crack,"J'ai découvert des informations surprenantes que j'aimerais que vous partagiez au grand public."),(sam,"Je vous écoute."),(crack,"Je me suis introduit dans le système informatique d'un hôpital dont je tairai le nom, et j'ai remarqué que les soignants enchaînaient les heures supplément-"),(sam,"-Pourquoi vous être introduit dans le système de l'hôpital ?"),(crack,"Vous n'avez pas besoin de savoir. Tout ce que vous devez savoir, c'est qu'en épluchant les dossiers médicaux, on observe qu'une maladie apparaît de plus en plus souvent. *RACCROCHE*"),(sam,"Mais...")});
        
        
        var state1Transitions = new Dictionary<int, int>
        {
            { 0, 2 },  // State 1 -> State 2
            { 1, 3 }   // State 1 -> State 3
        };

        var state2Transitions = new Dictionary<int, int>
        {
            { 0, 4 },  // State 2 -> State 4
            { 1, 5 }   // State 2 -> State 5
        };

        var state3Transitions = new Dictionary<int, int>
        {
            { 0, 5 },  // State 3 -> State 4
            { 1, 4 }   // State 3 -> State 5
        };

        var state4Transitions = new Dictionary<int, int>
        {
            { 0, 6 },  // State 2 -> End
            { 1, 6 }   // State 2 -> End
        };

        var state5Transitions = new Dictionary<int, int>
        {
            { 0, 6 },  // State 2 -> End
            { 1, 6 }   // State 2 -> End
        };

        // Add these dictionaries to the main dayTransitionMap
        dayTransitionMap.Add(1, state1Transitions);
        dayTransitionMap.Add(2, state2Transitions);
        dayTransitionMap.Add(3, state3Transitions);
        dayTransitionMap.Add(4, state4Transitions);
        dayTransitionMap.Add(5, state5Transitions);     

        // You can add more day-specific transition maps as needed...
    }

    // Method to get the current state
    public int GetCurrentState()
    {
        return currentDayState.state;
    }

    

    // Method to set the current state
    public void SetCurrentState(int newState)
    {
        currentDayState.state = newState;
        Debug.Log("State updated to: " + newState);
    }

    public void SetChoice(int choice)
    {
        currentDayState.choice = choice;
    }

    // Method to transition to the next state based on the current day and state
    public void TransitionToNextState()
    {
        int currentState = GetCurrentState();
        int currentChoice = currentDayState.choice;

        if (dayTransitionMap.ContainsKey(currentState))
        {
            Dictionary<int, int> stateTransitionMap = dayTransitionMap[currentState];

            if (stateTransitionMap.ContainsKey(currentChoice))
            {
                int nextState = stateTransitionMap[currentChoice];
                SetCurrentState(nextState);
                Debug.Log("Transitioned to next state: " + nextState);
            }
            else
            {
                Debug.LogWarning("No transition found for current state: " + currentState + " on Day " + currentChoice);
            }
        }
        
        // Load the Main scene after transitioning to the next state
        SceneManager.LoadScene("Main");

        mainIsLoading = true;
        isButtonReady = false;
    }
    
    


    private void Update()
    {
        if (button1 == null || button2 == null || button3 == null || cancelFolder == null || cancelFolder == null || folderButton == null)
        {
            button1 = GameObject.FindWithTag("button1");
            button2 = GameObject.FindWithTag("button2");
            button3 = GameObject.FindWithTag("button3");
            cancelMessage = GameObject.FindWithTag("closeMessage");
            cancelFolder = GameObject.FindWithTag("closeFolder");
            folderButton = GameObject.FindWithTag("Folder");

            if (cancelMessage != null && cancelFolder != null && folderButton != null)
            {
                cancelMessage.GetComponent<Button>().onClick.AddListener(delegate {
                    for (int i = 0; i < messageHolder.transform.childCount; i++)
                    {
                        messageHolder.transform.GetChild(i).gameObject.SetActive(false);
                    }
                });
                
                cancelFolder.GetComponent<Button>().onClick.AddListener(delegate {
                    for (int i = 0; i < pagesHolder.transform.childCount; i++)
                    {
                        pagesHolder.transform.GetChild(i).gameObject.SetActive(false);
                    }
                });
                
                foreach (var o in GameObject.FindGameObjectsWithTag("UI"))
                {
                    o.SetActive(false);
                    
                }
                cancelFolder.GetComponent<Button>().onClick.AddListener(delegate {desk.transform.SetAsLastSibling();});
                cancelMessage.GetComponent<Button>().onClick.AddListener(delegate {desk.transform.SetAsLastSibling();});
                folderButton.GetComponent<Button>().onClick.AddListener(delegate {desk.transform.SetSiblingIndex(1);});
                folderButton.GetComponent<Button>().onClick.AddListener(delegate { onFolderClick();});
            }
        }

        if (!isButtonReady)
        {
            if (button1 != null && button2 != null && button3 != null)
            {
                button1.GetComponent<Button>().onClick.AddListener(delegate {onButtonClick(1);});
                button2.GetComponent<Button>().onClick.AddListener(delegate {onButtonClick(2);});
                button3.GetComponent<Button>().onClick.AddListener(delegate {onButtonClick(3);});

                isButtonReady = true;
            }
        }

        desk = GameObject.FindWithTag("Desk");
        pagesHolder = GameObject.FindWithTag("PageHolder");
        messageHolder = GameObject.FindWithTag("MessageHolder");
        
        
            

        mainIsLoading = false;
    }

    public static void initGame()
    {
        instance.SetCurrentState(1);
        SceneManager.LoadScene("Main");
        mainIsLoading = true;
        isButtonReady = false;

    }

    public void onButtonClick(int button)
    {
        desk.transform.SetSiblingIndex(1);
        foreach ((Sprite spr, string str) in getDialog(button))
        {
            GameObject gameObject = Instantiate(messagePrefab, messageHolder.transform);
            var textmesh = gameObject.GetComponentInChildren<Text>();
            var image = gameObject.transform.GetChild(1).GetComponent<Image>();
            textmesh.text = str;
            image.sprite = spr;
        }
    }

    public void onFolderClick()
    {
        desk.transform.SetSiblingIndex(1);
            foreach (string s in getFolder())
            {
                GameObject gameObject = Instantiate(pagePrefab, pagesHolder.transform);
                var textmesh = gameObject.GetComponentInChildren<Text>();
                textmesh.text = s;
            }  
    }

    public List<(Sprite,String)> getDialog(int button)
    {
        int show = 10* GetCurrentState() + button;
        return dialogues[show];
    }


    public string getDebrief()
    {
        int show = 10 * GetCurrentState() + currentDayState.choice;
        return debriefs[show];
    }

    public string[] getFolder()
    {
        int show = GetCurrentState();
        return folders[show];
    }

    public void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}