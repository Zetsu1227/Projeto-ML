using UnityEngine;

public class AgentController : MonoBehaviour
{

    [Header("Columns")]
    [SerializeField]
    private Transform[] columnPositions;

    [Header("Distance")]
    [SerializeField]
    private float nearDistance = 2f;

    [SerializeField]
    private float mediumDistance = 5f;

    private int currentColumn;
    private State currentState;

    private void Start()
    {
        currentColumn = GetAgentColumn();
        currentState = ObserveState();
    }


    private State ObserveState()
    {
        State state = new State();

        state.agentColumn = currentColumn;

        state.column0 = GetObstacleInfo(0);
        state.column1 = GetObstacleInfo(1);
        state.column2 = GetObstacleInfo(2);
        state.column3 = GetObstacleInfo(3);
        state.column4 = GetObstacleInfo(4);

        return state;
    }

    private ObstacleInfo GetObstacleInfo(int column)
    {
        ObstacleInfo info = new ObstacleInfo();

        // Recompensa 0 significa que não existe obstáculo.
        info.recompensa = 0;
        info.distance = 0;

        float closestDistance = Mathf.Infinity;

        // Procura todos os obstáculos que existem atualmente na cena.
        Obstacle[] obstacles = FindObjectsByType<Obstacle>(FindObjectsSortMode.None);

        foreach (Obstacle obstacle in obstacles)
        {
            int obstacleColumn = GetObstacleColumn(obstacle);

            // Ignora obstáculos que estão em outra coluna.
            if (obstacleColumn != column)
                continue;

            // Distância vertical entre o obstáculo e o agente.
            float distance = Mathf.Abs(
                obstacle.transform.position.y -
                transform.position.y
            );

            // Se este for o obstáculo mais próximo da coluna,
            // ele será utilizado no State.
            if (distance < closestDistance)
            {
                closestDistance = distance;

                info.recompensa = obstacle.recompensa;
                info.distance = DiscretizeDistance(distance);
            }
        }

        return info;
    }

    
    private int GetObstacleColumn(Obstacle obstacle)
    {
        int closestColumn = 0;
        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < columnPositions.Length; i++)
        {
            float distance = Mathf.Abs(
                obstacle.transform.position.x -
                columnPositions[i].position.x
            );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestColumn = i;
            }
        }

        return closestColumn;
    }


    private int DiscretizeDistance(float distance)
    {
        if (distance <= nearDistance)
            return 2;

        if (distance <= mediumDistance)
            return 1;

        return 0;
    }


    private int GetAgentColumn()
    {
        int closestColumn = 0;
        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < columnPositions.Length; i++)
        {
            float distance = Mathf.Abs(
                transform.position.x -
                columnPositions[i].position.x
            );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestColumn = i;
            }
        }

        return closestColumn;
    }


    private void ExecuteAction(ActionType action)
    {
        switch (action)
        {
            case ActionType.Left:
                MoveLeft();
                break;

            case ActionType.Right:
                MoveRight();
                break;

            case ActionType.Stay:
                break;
        }
    }

    private void MoveLeft()
    {
        if (currentColumn <= 0)
            return;

        currentColumn--;

        MoveToColumn(currentColumn);
    }

    private void MoveRight()
    {
        if (currentColumn >= columnPositions.Length - 1)
            return;

        currentColumn++;

        MoveToColumn(currentColumn);
    }

    private void MoveToColumn(int column)
    {
        transform.position = new Vector3(
            columnPositions[column].position.x,
            transform.position.y,
            transform.position.z
        );
    }
}