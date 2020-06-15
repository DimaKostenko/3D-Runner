# 3D-Runner
3D runner example project

    Структура проекта:
1. Класс "GameManager" - отвечает за переключение между состояниями игры, а именно:
    - класс "PreGameState" (стартовое меню)
    - класс "GameState" (ингейм)
    - класс "GameOverState" (ендгейм)
2. Класс "TrackManager" - создание пути персонажа
3. Класс "DataManager" - доступ к сохранениям
4. Класс "GameStorage" - статический синглтон, хранит ссылку на класс с сохранениями (класс "DataManager"), а также на глобально оспользуемые в проекте объекты (в моем случае это класс "GameState")


    Примененные знания ООП:
1. Абстрактный класс "State" (и наследование от него "PreGameState", "GameState", "GameOverState")
2. Базовый класс "BarrierBase" (и наследование от него "BarrierPunch", "BarrierRandom"), так же:
    - Переопределение метода "OnPlayerCollisionEnter()" в классах "BarrierPunch" и "BarrierRandom"
    - Использован интерфейс "IBarrier";
3. Перегрузка методов в классе "DataManager"