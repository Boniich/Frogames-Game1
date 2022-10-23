using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public static LevelGenerator sharedInstance;
    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock> (); // contiene todos los niveles creados
    public List<LevelBlock> currentLevelBlock = new List<LevelBlock> () ; // lista de todos los bloques que tenemos en pantall
    public Transform levelInitialPoint; // punto inicial donde empezara a crearse el primer nivel
    private bool isGeneratingInitialBlocks = true;
    private int blockCount = 2; // define la cantidad de bloques que se van a generar

     void Awake()
    {
        sharedInstance = this;
       

    }
    // Start is called before the first frame update
    void Start()
    {
        GenerateInitialBlocks(blockCount);
        isGeneratingInitialBlocks = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateInitialBlocks(int blockCount)
    {
        //isGeneratingInitialBlocks = true;
        for(int i = 0; i < blockCount; i++)
        {
            AddNewBlock();
        }
       // isGeneratingInitialBlocks = false;
    }

    public void AddNewBlock()
    {
        // seleccionamos un bloque aleatorio entre los que tenemos disponibles
        int randomIndex = Random.Range(0, allTheLevelBlocks.Count);

        if (isGeneratingInitialBlocks)
        {
            randomIndex = 0;
        }

        Debug.Log("n° bloque: " + randomIndex);

        //creamos una instancia de uno de los elementos de la lista 
        LevelBlock block = (LevelBlock)Instantiate(allTheLevelBlocks[randomIndex]);
        block.transform.SetParent(this.transform,false);


        // posicion del bloque

        Vector3 blockPosition = Vector3.zero;

        if(currentLevelBlock.Count == 0)
        {
            //colocamos el primer bloque en la pantalla
            blockPosition = levelInitialPoint.position;
        }
        else
        {
            //ya tenemos bloques en pantalla, los unimos al ultimo disponible
            blockPosition = currentLevelBlock[currentLevelBlock.Count-1].exitPoint.position;
        }

        block.transform.position = blockPosition;
        currentLevelBlock.Add(block);
        
    }

    public void RemoveOldBlock()
    {
        LevelBlock block = currentLevelBlock[0];
        
        currentLevelBlock.Remove(block);
        // destruimos el primer bloque de la pantalla
        Destroy(block.gameObject);
       
    }

    public void RemoveAllTheBlocks()
    {
        while(currentLevelBlock.Count > 0)
        {
            RemoveOldBlock();
        }
    }
}
