using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManagment : Singleton<GameManagment>
{
    public TowerButton ClickButton { get; set; }

    public ObjectPool Pool{ get; set; }

    // Start is called before the first frame update
    private Tower selectedTower;

    [SerializeField]
    private GameObject gameOverMenu;

    [SerializeField]
    private GameObject systemMenu;

    private bool gameOver = false;

    [SerializeField]
    private Canvas menu;


    [SerializeField]
    private GameObject upgradePanel;

    [SerializeField]
    private Text sellText;

    private int cpu;
    private int disk;
    private int memory;

    private int towerPriceTax;


    public int TowerPriceTax
    {

        get
        {
            return towerPriceTax;
        }
        set
        {
            this.towerPriceTax = value;
       
        }

    }


    private int health = 50;
    private int bytes;
    public int Bytes {

        get
        { 
            return bytes;
        }
        set 
        { 
            this.bytes = value;
            this.byteText.text = " <color=white>Bytes: </color>" + value.ToString();
        }
            
    }

    public int Cpu
    {

        get
        {
            return cpu;
        }
        set
        {



            string damageLevel = "white";

            if(value >= 100)
            {
                this.cpu = 99;
            }
            else
            {
                this.cpu = value;
            }
           

            if (value < 40)
            {
                damageLevel = "green";
                

          
            }
            else if (value >= 40 && value < 70)
            {
                damageLevel = "yellow";
             
          
            }
            else if (value >= 70)
            {
                damageLevel = "red";
              
              
            }
       
            //this.cpuTxt.text = "CPU: <color=" + damageLevel + ">" + value.ToString() + "</color>%";
            this.cpuTxt.text = "CPU: <color=" + damageLevel + ">" + (value).ToString() + "</color>%";

        }

    }

    public int Disk
    {

        get
        {
            return disk;
        }
        set
        {



            string damageLevel = "white";
            if (value >= 100)
            {
                this.disk = 99;
            }
            else
            {
                this.disk = value;
            }

            if (value < 40)
            {
                damageLevel = "green";
            }
            else if (value >= 40 && value < 70)
            {
                damageLevel = "yellow";

            }
            else if (value >= 70)
            {
                damageLevel = "red";

            }



          
            //this.cpuTxt.text = "CPU: <color=" + damageLevel + ">" + value.ToString() + "</color>%";
            this.diskTxt.text = "DISK: <color=" + damageLevel + ">" + (value).ToString() + "</color>%";
        }

    }

    public int Memory
    {

        get
        {
            return memory;
        }
        set
        {



            string damageLevel = "white";
            if (value >= 100)
            {
                this.memory = 99;
            }
            else
            {
                this.memory = value;
            }

            if (value < 40)
            {
                damageLevel = "green";
            }
            else if(value >= 40 && value < 70)
            {
                damageLevel = "yellow";

            }
            else if (value >= 70)
            {
                damageLevel = "red";

            }



            //this.cpuTxt.text = "CPU: <color=" + damageLevel + ">" + value.ToString() + "</color>%";
            this.memoryTxt.text = "MEMORY: <color=" + damageLevel + ">" + (value).ToString() + "</color>%";
        }

    }

    public bool WaveActive
    {

        get
        {
            return activeViruses.Count > 0;
        }
    

    }



    private int wave = 0;

    [SerializeField]
    private Text sysText;

    [SerializeField]
    private Text diskTxt;

    [SerializeField]
    private Text cpuTxt;

    [SerializeField]
    private Text memoryTxt;


    int towersBought;



    private int systemIntegrity;

    public int SystemIntegrity
    {

        get
        {
            return systemIntegrity;
        }
        set
        {
            this.systemIntegrity = value;
      
            if(systemIntegrity <=0)
            {
                this.systemIntegrity = 0;
                GameOver();
            }
            this.sysText.text = systemIntegrity.ToString() + "%";
        }

    }

    [SerializeField]
    private Text waveText;

    [SerializeField]
    private Text byteText;


    

    [SerializeField]
    private GameObject waveBtn;

    [SerializeField]
    private GameObject menuBtn;

    private List<Viruses> activeViruses = new List<Viruses>();

    private void Awake()
    {

        Pool = GetComponent<ObjectPool>();

    }


    void Start()
    {
        Bytes = 500;
        SystemIntegrity = 100;
        Disk = 4;
        Memory = 3;
        Cpu = 3;
        towersBought = 0;


       
    }



    // Update is called once per frame
    void Update()
    {
      
        Cpu = Cpu;
        Memory = Memory;
        Disk = Disk;
        HandleEscape();

        
    }


    public void DisplayTowers()
    {

    }

    public void OpenSystemMenu()
    {
        if (systemMenu.active)
        {
            systemMenu.SetActive(false);
        }
        else
        {
            systemMenu.SetActive(true);
        }
       
    }

    public void ReduceCpu()
    {
        if(Bytes < 300 || Cpu == 3)
        {
            return;
        }
        else
        {
            Bytes = Bytes - 300;
            if (Cpu <= 25)
            {
                Cpu = 3;
            }
            else
            {
                Cpu = Cpu - 25;
            }
        } 

       



    }

    public void ReduceMemory()
    {

        if(Bytes < 300 || Memory == 3)
        {
            return;
        }
        else
        {
            Bytes = Bytes - 300;
            int towerSpace;
            towerSpace = towersBought * 5;
            Memory = 3 + towerSpace;
         
           
        }




    }

    public void ReduceDisk()
    {

        if(Bytes < 300 || Disk == 3)
        {
            return;
        }
        else
        {
            Bytes = Bytes - 300;
            if (Disk <= 25)
            {
                Disk = 3;
            }
            else
            {
                Disk = Disk - 25;
            }

        }




    }

    public void IncreaseHealth()
    {

        if(Bytes <150 || SystemIntegrity == 100)
        {
            return;
        }
        else
        {
            Bytes = Bytes - 150;
            if ((SystemIntegrity + 25) > 100)
            {
                SystemIntegrity = 100;
            }
            else
            {
                SystemIntegrity = SystemIntegrity + 25;
            }
        }
      





    }


    public void PickTower(TowerButton towerButton)
    {
        
        if(Bytes >= towerButton.Bytes && !WaveActive && (Memory + 5) <= 99)  
        {
            this.ClickButton = towerButton;
            Hover.Instance.Activate(towerButton.Sprite);
        }
  
    }

    public void BuyTower()
    {
     
        if(Bytes >= ClickButton.Bytes)
        {
            Bytes -= ClickButton.Bytes;
        }
        Hover.Instance.Deactivate();
        towersBought++;
        Memory = Memory + 5;

    }

    private void HandleEscape()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Deactivate();
        }


    }

    public void StartWave()
    {
        wave++;
        waveText.text = "Wave: <color=lime>" + wave + "</color>";
        StartCoroutine(SpawnWave());

        waveBtn.SetActive(false);
        menuBtn.SetActive(false);
        systemMenu.SetActive(false);
    }

    private IEnumerator SpawnWave()
    {
        int additionalHealth = 0;
        LevelManagement.Instance.GeneratePath();
        for (int i = 0; i < (wave + 1) * 2; i++)
        {

           
            int monsterIndex = Random.Range(0, 4);

            string type = string.Empty;


            switch (monsterIndex)
            {

                case 0:
                    type = "BugV";
                    health = 100;
                    break;
                case 1:
                    type = "WormV";
                    health = 125;
                    break;
                case 2:
                    type = "BotV";
                    health = 200;
                    break;
                case 3:
                    type = "SpyV";
                    health = 225;
                    break;
            }

            Viruses virus = Pool.GetObject(type).GetComponent<Viruses>();
            virus.Spawn(health + additionalHealth);

            if(wave % 3 == 0)
            {
                additionalHealth += 25;
            }
            activeViruses.Add(virus);

            yield return new WaitForSeconds(2.5f);


        }



        

    }


    public void RemoveVirus(Viruses virus)
    {

        activeViruses.Remove(virus);

        if(!WaveActive && !gameOver)
        {

            waveBtn.SetActive(true);
            menuBtn.SetActive(true);
        }
    }


    public void GameOver()
    {
        if(!gameOver)
        {
            gameOver = true;
            gameOverMenu.SetActive(true);
        }
       
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }



    public void SelectTower(Tower tower)
    {
       
        if(selectedTower != null)
        {
            selectedTower.Select();
        }
     
        selectedTower = tower;
        selectedTower.Select();
        sellText.text = " + " + (selectedTower.Price / 2).ToString();
        upgradePanel.SetActive(true);
    }

    public void Deselected()
    {
        if(selectedTower != null)
        {

            selectedTower.Select();
        }
        upgradePanel.SetActive(false);
        selectedTower = null;
    }

    public void SellTower()
    {
        if(selectedTower != null)
        {
            Bytes += selectedTower.Price / 2;
            selectedTower.GetComponentInParent<TileScript>().isEmpty = true;
            Destroy(selectedTower.transform.parent.gameObject);
            Deselected();
               
        }
    }
}
