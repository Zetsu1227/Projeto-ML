using System.Collections.Generic;
using UnityEngine;

public class QLearning : MonoBehaviour
{
    // ======================================================================================================================
    //======================================================== Dados ========================================================
    // ======================================================================================================================

    // Os hiperparâmetros são padrão -> GPT recomendou esses valores
    // O ideal é nós otimizarmos por meio de testes
    [Header("Parâmetros de aprendizagem")]
    [SerializeField] private float learningRate = 0.1f;
    [SerializeField] private float discountFactor = 0.95f;
    [SerializeField] private float explorationRate = 1.0f;
    [SerializeField] private float minExplorationRate = 0.05f;
    [SerializeField] private float explorationDecay = 0.995f;

    private readonly ActionType[] actions =
    {
        ActionType.Left,
        ActionType.Right,
        ActionType.Stay
    };

    // Cada estado terá um valor Q para cada ação.
    // Estrutura de dados principal do algoritmo
    private readonly Dictionary<string, float[]> qTable = new Dictionary<string, float[]>();


    // ======================================================================================================================
    // ======================================================= Métodos ======================================================
    // ======================================================================================================================


    // Escolhe a próxima ação do agente com base no estado atual,
    // equilibrando exploração (escolher uma ação aleatória) e aproveitamento (escolher a ação com maior valor Q)
    public ActionType ChooseAction(State state)
    {
        string stateKey = GetStateKey(state);
        EnsureStateExists(stateKey);

        // Exploração: escolher uma ação aleatória
        if (Random.value < explorationRate)
            return actions[Random.Range(0, actions.Length)];
        

        // Aproveitamento: escolher a ação com maior valor Q
        float[] values = qTable[stateKey];
        int bestAction = 0;

        for (int i = 1; i < values.Length; i++)
        {
            if (values[i] > values[bestAction])
                bestAction = i;
        }

        return actions[bestAction];
    }


    // Atualiza o valor Q da ação executada com base na recompensa recebida,
    // no valor Q anterior e na melhor recompensa futura estimada, utilizando a fórmula do Q-Learning
    public void Learn(State state, ActionType action, float reward, State nextState, bool episodeEnded)
    {
        string stateKey = GetStateKey(state);
        string nextStateKey = GetStateKey(nextState);

        EnsureStateExists(stateKey);
        EnsureStateExists(nextStateKey);

        int actionIndex = System.Array.IndexOf(actions, action);

        float oldQ = qTable[stateKey][actionIndex];

        float maxNextQ = 0f;

        if (!episodeEnded)
        {
            float[] nextValues = qTable[nextStateKey];
            maxNextQ = nextValues[0];

            for (int i = 1; i < nextValues.Length; i++)
            {
                if (nextValues[i] > maxNextQ)
                    maxNextQ = nextValues[i];
                
            }
        }

        // Fórmula de atualização do Q-Learning
        float newQ = oldQ + learningRate * (reward + discountFactor * maxNextQ - oldQ);

        qTable[stateKey][actionIndex] = newQ;
    }

    
    // Reduz gradualmente a explorationRate a cada chamada, respeitando o limite mínimo definido por minExplorationRate
    public void DecayExploration()
    {
        explorationRate = Mathf.Max(minExplorationRate, explorationRate * explorationDecay);
    }


    // Verifica se o estado já existe na tabela Q e, caso não exista, cria uma entrada
    // com os valores Q inicialmente iguais a zero para todas as ações possíveis
    private void EnsureStateExists(string stateKey)
    {
        if (!qTable.ContainsKey(stateKey))
            qTable[stateKey] = new float[actions.Length];
    }


    // Converte o estado atual do agente em uma string única, combinando sua posição e as informações de
    // recompensa e distância dos obstáculos nas cinco colunas, para utilizá-la como chave na tabela Q
    private string GetStateKey(State state)
    {
        return
            $"{state.agentColumn}|" +
            $"{state.column0.recompensa},{state.column0.distance}|" +
            $"{state.column1.recompensa},{state.column1.distance}|" +
            $"{state.column2.recompensa},{state.column2.distance}|" +
            $"{state.column3.recompensa},{state.column3.distance}|" +
            $"{state.column4.recompensa},{state.column4.distance}";
    }
}