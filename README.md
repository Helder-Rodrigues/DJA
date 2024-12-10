# Técnicas de IA
Este documento explica as técnicas de Inteligência Artificial (IA) implementadas no nosso jogo desenvolvido em Unity. O jogo utiliza três abordagens principais para oferecer uma experiência desafiadora e dinâmica para os jogadores. As técnicas incluem **MiniMax**, **Máquina de Estados** e **A (A Estrela)**.

## 1. MiniMax
O algoritmo MiniMax é utilizado no jogo para criar um sistema dinâmico de pontuação. Ele é usado para calcular a pontuação final após completar um circuito ou nível, proporcionando ao jogador um minijogo no qual pode interagir contra um oponente controlado pela IA (CPU). Nesse minijogo, o jogador deve tomar decisões estratégicas para determinar a quantidade de dinheiro que conseguirá ganhar. 

![WhatsApp Image 2024-12-09 at 23 11 38_2b703f4c](https://github.com/user-attachments/assets/8a1f780c-3c71-4f43-bbea-fb92ac8f3e07)

![WhatsApp Image 2024-12-09 at 23 11 48_6c669c0b](https://github.com/user-attachments/assets/489bd6de-3d3f-403d-bcb1-ce8afb688133)

## 2. Máquina de Estados
A Máquina de Estados é aplicada no controle do comportamento dos inimigos, durante o nível 2, com base em multiplos fatores como na detecção de um jogador proximo ou a proximidade de outros inimigos. 
Este sistema organiza o comportamento do inimigo em quatro estados principais, que são:
### Estados e Descrição:
#### Wander (Vaguear): O inimigo se move aleatoriamente pelo mapa. Esse estado é utilizado quando o inimigo não tem uma tarefa específica, proporcionando um movimento imprevisível.
#### ChasePlayer (Perseguir Jogador): O inimigo persegue o jogador quando este se encontra dentro de uma determinada distância de detecção. Esse estado é ativado assim que o jogador entra no alcance do inimigo.

![image](https://github.com/user-attachments/assets/e25dc575-b381-48da-9f9b-9319d5742866)

#### AvoidGhost (Evitar Fantasmas): O inimigo muda para este estado ao detectar a proximidade de outros fantasmas inimigos, movendo-se para uma posição que evite a colisão com eles. Isso assegura que os inimigos não se sobreponham.

![image](https://github.com/user-attachments/assets/3ac5273d-26f8-408e-ac8c-57293ea46d85)

#### Stuck (Preso): Este estado é ativado quando o inimigo permanece na mesma posição por um tempo prolongado. Neste estado ele move-se para uma posição aleatória próxima.

## 3. A*
O algoritmo A* é usado quando o inimigo está no estado Wander para encontrar o caminho mais curto em uma grid de nodes que representam as possíveis posições no jogo. Ele é essencial para a movimentação dos inimigos e determina a rota mais eficiente de um ponto a outro no mapa.
### Como Funciona:
A grid é criada automaticamente colocando e conectando os nodes uns aos outros de uma ponta até a outra do mapa, e em seguida o algoritmo é iniciado.

![image](https://github.com/user-attachments/assets/d57bd854-30d7-4abd-93a9-ead979abca30)

#### Inicialização: A lista de nós abertos é criada e todos os nós são inicializados com uma pontuação gScore infinita.
#### Escolha do Nó de Menor Custo: O nó com o menor custo é selecionado e removido da lista de nós abertos.
#### Exploração de Nós Vizinhos: Os nós vizinhos são avaliados e, se o caminho através do nó atual for mais curto, suas pontuações são atualizadas e os nós são adicionados à lista de nós abertos.
#### Caminho Encontrado: Quando o nó final é alcançado, o caminho é reconstruído, indo de volta ao nó inicial.

![image](https://github.com/user-attachments/assets/742ec796-9570-4d7c-8995-ad7aa8df9f29)
