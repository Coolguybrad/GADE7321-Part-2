using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    public static PieceManager Instance;
    // Start is called before the first frame update
    [SerializeField] private Piece selectedPiece;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedPiece != null)
        {
            BoardManager.Instance.showPossibleMoves(selectedPiece);
        }
        else
        {

            BoardManager.Instance.wipePossibleMoves();
        }
    }

    public void setSelectedPiece(Piece piece) 
    {
        selectedPiece = piece;
    }
    public Piece getSelectedPiece() 
    {
        return selectedPiece;
    }

    public void MoveSelectedPiece() 
    {
        if (BoardManager.Instance.getClickedTile().getPossibleMove().activeInHierarchy)
        {
            selectedPiece.movePiece(BoardManager.Instance.getClickedTile().getLocation());
            BoardManager.Instance.wipePossibleMoves();
        }
    }


    

    
}
