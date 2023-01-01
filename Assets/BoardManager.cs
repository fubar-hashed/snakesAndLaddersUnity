using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    private int _size, _ladders, _snakes, _whoseTurnIsIt, _players;
    private Dictionary<int, int> ladders, snakes;
    private Dictionary<int, Tile> _tiles;
    private int lastNum, player1, player2, player3, player4;

    [SerializeField] private Tile _tilePreFab;
    [SerializeField] private GameObject _gridSize, _noLadders, _noSnakes, _noPlayers;
    [SerializeField] private Text dieOutput;
    [SerializeField] private Color _player1Color, _player2Color, _player3Color, _player4Color, _baseColor, _portalColor;

    public void start() {
    }
    
    void GenerateGrid() {
        _tiles = new Dictionary<int, Tile>();
        int number = 0;
        for (int x = 0; x < _size; x++) {
            for (int y = 0; y < _size; y++) {
                var spawnedTile = Instantiate(_tilePreFab, new Vector3(y-5, x-3), Quaternion.identity); // - _size/2 + 0.5f
                spawnedTile.name = $"Tile {number}";
                number++;

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(_baseColor, number);
                _tiles.Add(number, spawnedTile);
            }
        }
    }

    public void clearBoard() {
        try {
            foreach(int tileNumber in _tiles.Keys) {
                _tiles[tileNumber].destroy();
            }
        } catch {
            Debug.Log("No board found");
        }
    }

    public void createNewBoard(string s) {
        _size = int.Parse(_gridSize.GetComponent<Text>().text);
        _ladders = int.Parse(_noLadders.GetComponent<Text>().text);
        _snakes = int.Parse(_noSnakes.GetComponent<Text>().text);
        _players = int.Parse(_noPlayers.GetComponent<Text>().text);

        clearBoard();

        ladders = new Dictionary<int, int>();
        snakes = new Dictionary<int, int>();
        
        Debug.Log(_size.ToString() + " " + _ladders.ToString() + " " + _snakes.ToString());
        GenerateGrid();

        lastNum = _size*_size;

        for (int i = 0; i<_ladders; i++){
            var start = Random.Range(1, lastNum - _size);
            var end = Random.Range(start + (_size - (start%_size)), lastNum);
            if (snakes.ContainsKey(start) || ladders.ContainsKey(start) || snakes.ContainsKey(end) || ladders.ContainsKey(end)) {
                i--;
                continue;
            }
            ladders.Add(start, end);
            _tiles[start].updateColor(_portalColor);
        }

        for (int i = 0; i<_snakes; i++){
            var start = Random.Range(_size, lastNum);
            var end = Random.Range(1, start - (start%_size));
            if (snakes.ContainsKey(start) || ladders.ContainsKey(start) || snakes.ContainsKey(end) || ladders.ContainsKey(end)) {
                i--;
                continue;
            }
            snakes.Add(start, end);
            _tiles[start].updateColor(_portalColor);
        }
        _whoseTurnIsIt = 1;
        player1 = 1;
        player2 = 1;
        player3 = 1;
        player4 = 1;
        _tiles[getPlayer(_whoseTurnIsIt)].updateColor(getPlayerColor(_whoseTurnIsIt));
    }

    public int getPlayer(int number) {
        if (number == 1) {
            return player1;
        }
        if (number == 2) {
            return player2;
        }
        if (number == 3) {
            return player3;
        }
        if (number == 4) {
            return player4;
        }
        Debug.Log("getPlayer! not a vlaid player!!");
        return player1;
    }

    public Color getPlayerColor(int number) {
        if (number == 1) {
            return _player1Color;
        }
        if (number == 2) {
            return _player2Color;
        }
        if (number == 3) {
            return _player3Color;
        }
        if (number == 4) {
            return _player4Color;
        }
        Debug.Log("not a vlaid player!!");
        return _player1Color;
    }

    public void updatePlayer(int number, int value) {
        if (number == 1) {
            player1 = value;
        }
        if (number == 2) {
            player2 = value;
        }
        if (number == 3) {
            player3 = value;
        }
        if (number == 4) {
            player4 = value;
        }
    }

    public void playTurn() {
        var currPlayer = getPlayer(_whoseTurnIsIt);
        var prev = currPlayer;
        var dieRoll = Random.Range(1, 6);
        dieOutput.text = dieRoll.ToString();
        if (currPlayer + dieRoll <= lastNum) {
            currPlayer += dieRoll;
        }
        if (ladders.ContainsKey(currPlayer)) {
            currPlayer = ladders[currPlayer];
            Debug.Log("Player " + _whoseTurnIsIt + " Climbed a ladder!!!");
        }
        if (snakes.ContainsKey(currPlayer)) {
            currPlayer = snakes[currPlayer];
            Debug.Log("Player " + _whoseTurnIsIt + " Caught by a snake!!!");
        }
        if (currPlayer == lastNum) {
            Debug.Log("Player " + _whoseTurnIsIt + " wins!!!");
        }
        Debug.Log("Player " + _whoseTurnIsIt + "'s current value" + currPlayer.ToString());
        updatePlayer(_whoseTurnIsIt, currPlayer);
        _tiles[currPlayer].updateColor(getPlayerColor(_whoseTurnIsIt));

        updateTurnNumber();
        if (prev == player1) {
            _tiles[prev].updateColor(_player1Color);
        } else {
            if (prev == player2) {
                _tiles[prev].updateColor(_player2Color);
            } else {
                if (prev == player3) {
                    _tiles[prev].updateColor(_player3Color);
                } else {
                    if (prev == player4) {
                        _tiles[prev].updateColor(_player4Color);
                    } else {
                        _tiles[prev].updateColor(_baseColor);
                    }
                }
            }
        }
    }

    private void updateTurnNumber() {
        _whoseTurnIsIt++;
        _whoseTurnIsIt = _whoseTurnIsIt % _players;
        if (_whoseTurnIsIt == 0) {
            _whoseTurnIsIt = _players;
        }
        Debug.Log("Current turn :: " + _whoseTurnIsIt);
    }
}
