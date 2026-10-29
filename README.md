# Sobre o Projeto
É um projeto feito por membros do PET Computação da UFRGS para aprender na prática sobre Machine Learning especificamente
para estudar e aplicar diferentes métodos de aprendizado de forma lúdica para fins demonstrativos.
Atualmente, o projeto está focado em aplicar um método de aprendizagem por reforço chamado de Q-learning, porém o agente e a simulação serão modularizados para 
facilitar a expensão do projeto e a aplicação de diferentes métodos futuramente.

# O jogo
A ideia do jogo é simples, barris e bananas caem do céu, e o objetivo do jogador é tentar sobreviver o máximo de tempo possível
desviando dos barris e coletando as bananas para ganhar pontos.

# Do aprendizado (Machine learning)
## Agente
O agente será responsável pela observação do espaço da simulação e por aplicar as ações do jogador.

## Q-Learning
A IA será treinada usando o Q-learning, que é uma forma de aprendizado por reforço. Esse método tem como base recompensas e punições.
Cada ação é recompensada ou punida e é armazenado o quão bom ou ruim foi realizar aquela ação no chamado Q-table.
As decisões de quão ação realizar será feita a partir de uma fórmula que analisa o estado atual do jogador, as possíveis ações que ele pode realizar, a possível situação futura que o jogador estaria se ele fizesse essas ações, o quanto recompensas são apreciadas e uma taxa de curiosidade para encorajar difererentes ações.

# Atual estrutura da simulação
Com a modularização do projeto em mente, a simulação é separada em três componentes principais: O ambiente, o método de aprendizado(Q-learning) e o agente.
O ambiente é responsável pelo funcionamento básico do jogo, como cada objeto funciona, animações e como o jogador interage com o espaço.
O método de aprendizado é responsável por aplicar o Q-learning, tomando as decisões e armazenando os dados.
O agente é responsável pela comunicação entre o ambiente e o Q-learning, além de aplicar as decisões tomadas pelo método.

