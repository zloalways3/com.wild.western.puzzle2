using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerWestern : MonoBehaviour {
  private int difficultyWestern = 4;
  [SerializeField] private Transform gameHolderWestern;
  [SerializeField] private Transform piecePrefabWestern;

  [SerializeField] private List<Texture2D> imageTexturesWestern;
  [SerializeField] private Transform levelSelectPanelWestern;
  [SerializeField] private Image levelSelectPrefabWestern;

  private List<Transform> piecesWestern;
  private Vector2Int dimensionswestern;
  private float widthWestern;
  private float heightWestern;

  private Transform draggingPieceWestern = null;
  private Vector3 offsetWestern;

  private int piecesCorrectWestern;

  private int levelPuzzleWestern;

  [SerializeField] GameObject[] labelPuzzleWestern;
  [SerializeField] Image labelFirstPuzzleWestern;
    [SerializeField] Sprite[] spritesPuzzleWestern;

    [SerializeField] GameObject endBlockPuzzleWestern;
    [SerializeField] Image endLabelImageWestern;
    [SerializeField] Sprite winPuzzleSpriteWestern;
    [SerializeField] Sprite losePuzzleSpriteWestern;
    [SerializeField] Image puzzleIndexWestern;
    [SerializeField] Sprite[] westernSpritesIndex;

    private bool startedWestern;
    private bool endWestern;

    [SerializeField] AudioSource musicPuzzleWestern;
    [SerializeField] AudioSource soundsPuzzleWestern;
  void Start() {
        if (PlayerPrefs.GetFloat("musicWestern", 1f) == 1f) musicPuzzleWestern.Play();
        startedWestern = false;
        endWestern = false;
    levelPuzzleWestern = (int)PlayerPrefs.GetFloat("puzzleWestern", 0f);
        puzzleIndexWestern.sprite = westernSpritesIndex[levelPuzzleWestern];
    labelFirstPuzzleWestern.sprite = spritesPuzzleWestern[levelPuzzleWestern];
    foreach (Texture2D texture in imageTexturesWestern) {
      Image imageWestern = Instantiate(levelSelectPrefabWestern, levelSelectPanelWestern);
      imageWestern.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            // Assign button action
            print(texture.width);
      imageWestern.GetComponent<Button>().onClick.AddListener(delegate { StartGameWestern(texture); });
    }
  }

  public void nextPuzzleWestern()
    {
        levelPuzzleWestern++;
        levelPuzzleWestern %= spritesPuzzleWestern.Length;
        PlayerPrefs.SetFloat("puzzleWestern", levelPuzzleWestern);
        PlayerPrefs.Save();
        SceneManager.LoadScene(4);
    }  

  public void clickPuzzleWestern()
    {
        for (int iwestern = 0; iwestern < labelPuzzleWestern.Length; iwestern++) labelPuzzleWestern[iwestern].SetActive(false);
        StartGameWestern(imageTexturesWestern[levelPuzzleWestern]);
    }
    private float timePuzzleAftherStart;
  public void StartGameWestern(Texture2D jigsawTexture) {
    // Hide the UI
    levelSelectPanelWestern.gameObject.SetActive(false);
        startedWestern = true;
        timePuzzleAftherStart = timerWesten + 120;

    piecesWestern = new List<Transform>();

    dimensionswestern = GetDimensionsWestern(jigsawTexture, difficultyWestern);

    CreateJigsawPiecesWestern(jigsawTexture);

    ScatterWestern();

    UpdateBorder();

    piecesCorrectWestern = 0;
  }

  Vector2Int GetDimensionsWestern(Texture2D jigsawTexture, int difficulty) {
    Vector2Int dimensionsWestern = Vector2Int.zero;
    if (jigsawTexture.width < jigsawTexture.height) {
      dimensionsWestern.x = difficulty;
      dimensionsWestern.y = (difficulty * jigsawTexture.height) / jigsawTexture.width;
    } else {
      dimensionsWestern.x = (difficulty * jigsawTexture.width) / jigsawTexture.height;
      dimensionsWestern.y = difficulty;
    }
    return dimensionsWestern;
  }

  void CreateJigsawPiecesWestern(Texture2D jigsawTexture) {
    heightWestern = 1f / dimensionswestern.y;
    float aspectWestern = (float)jigsawTexture.width / jigsawTexture.height;
    widthWestern = aspectWestern / dimensionswestern.x;

    for (int rowwestern = 0; rowwestern < dimensionswestern.y; rowwestern++) {
      for (int colwestern = 0; colwestern < dimensionswestern.x; colwestern++) {
        Transform pieceWestern = Instantiate(piecePrefabWestern, gameHolderWestern);
        pieceWestern.localPosition = new Vector3(
          (-widthWestern * dimensionswestern.x / 2) + (widthWestern * colwestern) + (widthWestern / 2),
          (-heightWestern * dimensionswestern.y / 2) + (heightWestern * rowwestern) + (heightWestern / 2),
          -1);
        pieceWestern.localScale = new Vector3(widthWestern, heightWestern, 1f);

        pieceWestern.name = $"Piece {(rowwestern * dimensionswestern.x) + colwestern}";
        piecesWestern.Add(pieceWestern);

        float width1Western = 1f / dimensionswestern.x;
        float height1Western = 1f / dimensionswestern.y;
        Vector2[] uvWestern = new Vector2[4];
        uvWestern[0] = new Vector2(width1Western * colwestern, height1Western * rowwestern);
        uvWestern[1] = new Vector2(width1Western * (colwestern + 1), height1Western * rowwestern);
        uvWestern[2] = new Vector2(width1Western * colwestern, height1Western * (rowwestern + 1));
        uvWestern[3] = new Vector2(width1Western * (colwestern + 1), height1Western * (rowwestern + 1));
        Mesh meshWestern = pieceWestern.GetComponent<MeshFilter>().mesh;
        meshWestern.uv = uvWestern;
        pieceWestern.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", jigsawTexture);
      }
    }
  }

  // Place the pieces randomly in the visible area.
  private void ScatterWestern() {
    // Calculate the visible orthographic size of the screen.
    float orthoHeightWestern = Camera.main.orthographicSize;
    float screenAspectWestern = (float)Screen.width / Screen.height;
    float orthoWidthWestern = (screenAspectWestern * orthoHeightWestern);

    // Ensure pieces are away from the edges.
    float pieceWidthWestern = widthWestern * gameHolderWestern.localScale.x;
    float pieceHeightWestern = heightWestern * gameHolderWestern.localScale.y;

    orthoHeightWestern -= pieceHeightWestern;
    orthoWidthWestern -= pieceWidthWestern;

    // Place each piece randomly in the visible area.
    foreach (Transform pieceWestern in piecesWestern) {
      float xWestern = Random.Range(-orthoWidthWestern, orthoWidthWestern);
      float yWestern = Random.Range(-orthoHeightWestern, orthoHeightWestern);
      pieceWestern.position = new Vector3(xWestern, yWestern, -1);
    }
  }

  // Update the border to fit the chosen puzzle.
  private void UpdateBorder() {
    LineRenderer lineRendererWestern = gameHolderWestern.GetComponent<LineRenderer>();

    // Calculate half sizes to simplify the code.
    float halfWidthWestern = (widthWestern * dimensionswestern.x) *2;
    float halfHeightWestern = (heightWestern * dimensionswestern.y) *2;

    // We want the border to be behind the pieces.
    float borderZ = 0f;
    // Set border vertices, starting top left, going clockwise.
    lineRendererWestern.SetPosition(0, new Vector3(-halfWidthWestern, halfHeightWestern+1, borderZ));
    lineRendererWestern.SetPosition(1, new Vector3(halfWidthWestern, halfHeightWestern+1, borderZ));
    lineRendererWestern.SetPosition(2, new Vector3(halfWidthWestern, -halfHeightWestern+1, borderZ));
    lineRendererWestern.SetPosition(3, new Vector3(-halfWidthWestern, -halfHeightWestern+1, borderZ));

    // Set the thickness of the border line.
    lineRendererWestern.startWidth = 0.1f;
    lineRendererWestern.endWidth = 0.1f;

    // Show the border line.
    lineRendererWestern.enabled = true;
  }
    private float timerWesten = 0f;

  // Update is called once per frame
  void Update() {
    if(!startedWestern)    timerWesten += Time.deltaTime;
    else
        {
            if (timePuzzleAftherStart - timerWesten > 0)
            {
                timePuzzleAftherStart -= Time.deltaTime;
            }
            else if (!endWestern)
            {
                gameHolderWestern.gameObject.SetActive(false);
                endWestern = true;
                endBlockPuzzleWestern.SetActive(true);
                endLabelImageWestern.sprite = losePuzzleSpriteWestern;
            }
        }
    if (Input.GetMouseButtonDown(0)) {
      RaycastHit2D hitWestern = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
      if (hitWestern) {
        draggingPieceWestern = hitWestern.transform;
        offsetWestern = draggingPieceWestern.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offsetWestern += Vector3.back;
      }
    }

    if (draggingPieceWestern && Input.GetMouseButtonUp(0)) {
      SnapAndDisableIfCorrectWestern();
      draggingPieceWestern.position += Vector3.forward;
      draggingPieceWestern = null;
    }

    if (draggingPieceWestern) {
      Vector3 newPositionWestern = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      newPositionWestern += offsetWestern;
      draggingPieceWestern.position = newPositionWestern;
    }
  }

  private void SnapAndDisableIfCorrectWestern() {
    int pieceIndexWestern = piecesWestern.IndexOf(draggingPieceWestern);

    // The coordinates of the piece in the puzzle.
    int colWestern = pieceIndexWestern % dimensionswestern.x;
    int rowWestern = pieceIndexWestern / dimensionswestern.x;
    Vector2 targetPositionWestern = new Vector2((-widthWestern * dimensionswestern.x / 2) + (widthWestern * colWestern) + (widthWestern / 2),
                                 (-heightWestern * dimensionswestern.y / 2) + (heightWestern * rowWestern) + (heightWestern / 2));

    // Check if we're in the correct location.
    if (Vector2.Distance(draggingPieceWestern.localPosition, targetPositionWestern) < (widthWestern / 2)) {
      // Snap to our destination.
      draggingPieceWestern.localPosition = targetPositionWestern;

      draggingPieceWestern.GetComponent<BoxCollider2D>().enabled = false;

      piecesCorrectWestern++;
            if (PlayerPrefs.GetFloat("soundsWestern", 1f) == 1f) soundsPuzzleWestern.Play();
      if (piecesCorrectWestern == piecesWestern.Count) {
                endWestern = true;
                endBlockPuzzleWestern.SetActive(true);
      }
    }
  }
}
