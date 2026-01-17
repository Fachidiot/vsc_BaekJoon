#include <iostream>
#include <string>
#include <vector>
#include <thread>
#include <chrono>

#include "Map.h"
#include "Player.h"
#include "Ghost.h"
#include "common.hpp"
#include "ConsoleUtils.hpp"

// Game state enum
enum class GameStatus
{
    Running,
    End,
    GameOver
};

// Encapsulates the main game logic
class Game
{
private:
    Map map;
    Player player;
    std::vector<Ghost> ghosts;
    GameStatus status;
    std::string resourcePath;

    void init()
    {
        map = Map();
        player = Player(0, 3, Direction::Up);
        ghosts.assign(3, Ghost());

        map.readMap(resourcePath + "/pacman_stage1.txt");

        player.setStartPoint(map.getPoint(static_cast<int>(MapType::PLAYER)));
        ghosts[0].setStartPoint(map.getPoint(static_cast<int>(MapType::GHOST1)));
        ghosts[1].setStartPoint(map.getPoint(static_cast<int>(MapType::GHOST2)));
        ghosts[2].setStartPoint(map.getPoint(static_cast<int>(MapType::GHOST3)));

        resetPositions();
    }

    void resetPositions()
    {
        player.initPoint();
        for (auto &ghost : ghosts)
        {
            ghost.initPoint();
        }
    }

    void update()
    {
        while (true)
        {
            if (player.getPoint() >= map.getMaxPoint())
            {
                status = GameStatus::End;
                return;
            }

            for (const auto &ghost : ghosts)
            {
                if (player.detectGhost(ghost.getX(), ghost.getY()))
                {
                    if (player.getLife() <= 1)
                    {
                        status = GameStatus::GameOver;
                        return;
                    }
                    player.setLife(player.getLife() - 1);
                    resetPositions();
                }
            }

            // --- DRAW PHASE ---
            clearScreen();
            setCursorPosition(0, 0);
            std::cout << "SCORE : " << player.getPoint() << " / LIFE : " << player.getLife() << std::endl;
            map.printMap();
            player.printPlayer();
            for (const auto &ghost : ghosts)
            {
                ghost.printGhost();
            }
            std::cout.flush();

            // --- INPUT & UPDATE PHASE ---
            player.processInput();
            player.move(map);
            for (auto &ghost : ghosts)
            {
                ghost.move(map, player.getX(), player.getY());
            }

            std::this_thread::sleep_for(std::chrono::milliseconds(200));
        }
    }

public:
    Game(const std::string &resPath) : player(0, 3, Direction::Up), resourcePath(resPath) {}

    void run()
    {
        readAndWriteFile(resourcePath + "/Intro.txt");
        std::cin.get();

        bool loop = true;
        while (loop)
        {
            init();
            status = GameStatus::Running;
            update();

            switch (status)
            {
            case GameStatus::End:
                clearScreen();
                readAndWriteFile(resourcePath + "/Congratulations.txt");
                std::cin.get();
                loop = false;
                break;
            case GameStatus::GameOver:
                clearScreen();
                readAndWriteFile(resourcePath + "/GameOver.txt");
                std::cin.get();
                break;
            case GameStatus::Running:
                // Should not happen, but good practice
                loop = false;
                break;
            }
        }
    }
};

int main()
{
    // Assume resources are in a "resources" subdirectory of the executable's location.
    Game game("../resources");
    game.run();

    return 0;
}