# Lanes of Time

Um **jogo de estratégia em tempo real (RTS) para Android** desenvolvido em **Unity (C#)**.  
Duas bases de eras históricas diferentes lutam pelo controlo do tempo num campo dividido em cinco faixas de batalha.  
Disponível em **modo Campanha** (single-player contra IA) e **modo 1v1 local** (dois jogadores no mesmo dispositivo).

---

## Visão Geral

Lanes of Time foi desenvolvido como projeto final da unidade curricular de **Design de Jogos** no ISEL, evoluindo ao longo de vários meses desde o conceito inicial até à versão jogável final com testes de jogabilidade.  
O projeto focou-se em **design de mecânicas inovadoras**, **desenvolvimento de IA** e **otimização para dispositivos Android de gama média**.

| **Categoria** | **Descrição** |
|---|---|
| **Motor** | Unity (URP) |
| **Linguagem** | C# |
| **Plataforma** | Android (orientação horizontal) |
| **Modos de Jogo** | Campanha (vs IA) · 1v1 Local (ecrã partilhado) |
| **Duração por Partida** | ~5 minutos |
| **Tempo de Desenvolvimento** | 1 semestre (projeto individual) |

---

## Conceito

Em *Lanes of Time*, o jogador assume o papel de comandante de uma civilização capaz de viajar entre **cinco eras históricas distintas**: Pré-História, Antiguidade Clássica, Idade Média, Era Industrial e Futuro. O objetivo é simples: **destruir a base inimiga** enquanto proteges a tua.

O jogo rejeita a ideia de memorizar uma estratégia vencedora fixa. A **economia variável**, os **combos temporários** e a **audiência reativa** garantem que a melhor decisão depende sempre do estado atual do campo de batalha — premiando a improvisação, a criatividade e a adaptação.

---
 
## Screenshots
 
<p align="center">
  <img src="images/menu_inicial.png" width="49%" alt="Menu Inicial"/>
  <img src="images/menu_campanha.png" width="49%" alt="Menu Campanha"/>
</p>
<p align="center">
  <em>Menu Inicial (esquerda) · Menu Campanha (direita)</em>
</p>
 
<p align="center">
  <img src="images/menu_melhorias.png" width="49%" alt="Menu de Melhorias"/>
  <img src="images/gameplay.png" width="49%" alt="Gameplay"/>
</p>
<p align="center">
  <em>Menu de Melhorias (esquerda) · Gameplay em campo de batalha (direita)</em>
</p>
 
---

## Mecânicas Principais

### Sistema de Eras
Ao encher a barra de experiência, o jogador avança automaticamente para a era seguinte, desbloqueando:
- Um conjunto único de **três classes de tropas** (*melee*, *ranged*, especial) com estética adaptada ao período histórico.
- Um **evento catastrófico** exclusivo (asteroides, gladiadores em área, comboios, fumo tóxico, invocação em massa).
- Uma **base visual distinta** correspondente à era.

### Sistema Económico Dinâmico
O ouro é o recurso principal, obtido automaticamente ao longo do tempo. A inovação central está no **sistema de oferta e procura**: cada vez que uma tropa é invocada repetidamente, o seu custo aumenta temporariamente, enquanto as tropas menos usadas ficam mais baratas. Isto força o jogador a diversificar as suas escolhas em vez de repetir sempre a mesma unidade.

### Fusão de Tropas
Quando duas tropas da mesma classe se encontram na mesma faixa, **fundem-se automaticamente** numa unidade mais forte acompanhada de um efeito visual. Ao fundir tropas, o jogador aumenta a probabilidade de obter o apoio do público.

### Sistema de Combos
Durante cada partida, o jogo define **sequências específicas de invocação** para cada jogador. Executar essas sequências num curto intervalo de tempo completa um combo, garantindo recompensas em ouro ou experiência e aumentando a probabilidade de aprovação do público.

### Audiência Reativa
As bancadas de público 2D observam o combate e reagem positivamente a fusões, combos e recolha de *loot boxes*. Quando impressionada, a audiência oferece **recompensas aleatórias** (ouro extra, experiência bónus), adicionando uma camada de imprevisibilidade e criando objetivos secundários durante a partida.

### Loot Boxes
Ocasionalmente, caixas de *loot* caem aleatoriamente numa das faixas com paraquedas. O jogador que conseguir apanhá-las primeiro ganha ouro ou experiência extra — criando um objetivo secundário que obriga a arriscar.

---

## Implementação Técnica

### Scripts Principais

**`GameManager.cs`**  
Responsável pelo estado geral da partida. Controla as eras disponíveis, o spawn de tropas nas cinco faixas, os modos de jogo, os eventos especiais por era e as condições de vitória.

**`PlayerScript.cs`**  
Gere os recursos e a progressão de cada jogador. Controla o ouro, a experiência, a barra de XP e as transições de era.

**`UIManager.cs`**  
Centraliza a interface e o input dos jogadores. Gere os botões de tropas, seleção de faixas, *cooldowns* visuais, preços dinâmicos, o temporizador e o ecrã de fim de partida com estatísticas.

**`CharacterScript.cs`**  
Controla o comportamento individual de cada unidade: movimento automático, ataques (*melee* ou *ranged*), gestão de vida, recompensas ao adversário ao morrer, **fusão automática** de unidades e gestão de alvos na faixa.

### Arquitetura de Scripts

```text
Scripts/
├── GameManager.cs
├── PlayerScript.cs
├── UIManager.cs
└── CharacterScript.cs
```

### Deteção de Colisões
Utiliza o sistema de *colliders* e *triggers* do Unity. *Colliders* físicos garantem que tropas e projéteis interagem com o chão e as bases. *Triggers* são usados para alcance de ataque, fusão de tropas e recolha de *loot boxes*. O sistema de *tags* diferencia tropas de cada base, prevenindo *friendly fire*.

### IA Adversária (Modo Campanha)
O oponente é controlado por IA com parâmetros que escalam progressivamente ao longo dos 15 níveis, aumentando valores de vida, ataque e velocidade das tropas. A IA replica as mesmas mecânicas disponíveis ao jogador — invocação de tropas, mudança de era e uso de eventos catastróficos.

### Sistema de Renderização
O jogo utiliza um **mundo 3D com câmara ortográfica fixa** e interface 2D sobreposta. Bases, tropas e *loot boxes* são modelos 3D *low-poly*. A audiência é uma textura 2D em bancadas no fundo. Toda a UI (barras, botões, temporizador) é construída com Canvas 2D.

---

## Design Visual

### Identidade por Era
Cada era tem uma paleta de cores e um cenário próprios, reforçando a identidade emocional de cada período:

| **Era** | **Paleta** | **Atmosfera** |
|---|---|---|
| Pré-História | Castanho · Laranja queimado | Quente, primitivo, orgânico |
| Antiguidade | Dourado · Bege · Amarelo-claro | Glorioso, equilibrado, árido |
| Idade Média | Verde-escuro · Castanho · Vermelho-escuro | Pesado, rústico, conflituoso |
| Industrial | Cinzento · Cinzento-azulado · Ferrugem | Mecânico, desgastado, progresso |
| Futuro | Azul-escuro · Azul elétrico · Ciano | Frio, tecnológico, digital |

### Conceito Visual
O jogo é apresentado como um **coliseu temporal** onde civilizações de eras diferentes são convocadas para combater. No modo 1v1, a arena é neutra com bancadas de público 2D. No modo Campanha, cada batalha decorre num cenário temático coerente com a era do nível.

---

## Evolução do Design

O conceito de *Lanes of Time* passou por várias iterações ao longo do semestre:

**Versão 1.00** — Conceito base com batalhas em faixas entre eras históricas, economia fixa e *loot boxes*. Inspirado em *Age of War* e *Warlords: Call to Arms*.

**Versão 1.10** — Após inquérito a colegas com escala de Likert, a inovação foi identificada como o ponto mais fraco. Foram introduzidos: **sistema económico de oferta e procura**, **fusão de tropas**, **combos** e **audiência reativa**. Removidos elementos pouco coesos (pedómetro, portais aleatórios).

**Versão 2.00** — Consolidação com *concept art*, *mood boards*, *color script* e identidade visual por era. Cinco eras completas com tropas, bases e eventos catastróficos únicos.

---

## Testes de Jogabilidade

O modo Campanha foi avaliado com **10 participantes** através de questionário online e observação direta.

| **Métrica** | **Resultado** |
|---|---|
| Nota geral da experiência | **7.4 / 10** |
| Intenção de recomendar o jogo | **100%** |
| Intenção de voltar a jogar | **80%** |
| Responsividade dos controlos | **4.6 / 5** |
| Decisões com impacto no resultado | **80% positivo** |
| Nível médio atingido | **7.7 / 15** |

**Pontos fortes identificados:** Diversidade de eventos por era, progressão através das eras históricas, ritmo das partidas e aspeto visual.

**Principais áreas de melhoria:** Clareza da economia dinâmica, tamanho das zonas de toque, curva de aprendizagem inicial e distinção entre tipos de tropas.

---

## Funcionalidades

- Cinco eras jogáveis com bases, tropas e eventos distintos.
- Campo de batalha com cinco faixas paralelas.
- Sistema económico dinâmico com custo variável baseado em oferta e procura.
- *Loot boxes* com paraquedas que caem aleatoriamente.
- Fusão automática de tropas do mesmo tipo.
- Sistema de combos com recompensas.
- Audiência reativa a jogadas marcantes.
- Modo 1v1 local em ecrã partilhado.
- Partidas com temporizador de 5 minutos e ecrã de estatísticas.
- Sistema de melhorias permanentes entre níveis (ataque, defesa, velocidade, audiência, eventos).

---

## Desafios & Soluções

**Desafio:** Comunicar a economia dinâmica de forma clara sem interromper o fluxo da partida.  
**Solução:** Tutorial dedicado à mecânica, indicadores visuais nos preços das tropas e pop-ups contextuais durante a partida.

**Desafio:** Equilibrar a dificuldade da IA para ser desafiante sem ser frustrante para jogadores casuais.  
**Solução:** Escalonamento progressivo dos parâmetros da IA ao longo dos 15 níveis, com curva mais suave nos primeiros três níveis.

**Desafio:** Manter a legibilidade da interface com múltiplas tropas, faixas, pop-ups e botões no mesmo ecrã de telemóvel.  
**Solução:** Hierarquia visual clara com elementos 3D para componentes de jogo e UI 2D para informação, câmara ortográfica fixa e paleta de cores distinta por categoria de botão.

**Desafio:** Implementar um modo 1v1 local num único dispositivo sem que os dois jogadores interferiram nos controlos um do outro.  
**Solução:** Interface espelhada com botões de cada jogador posicionados em lados opostos do ecrã e *tags* a diferenciar ações de cada lado.

---

## Ligações
[Ver o meu LinkedIn](https://www.linkedin.com/in/rgtd-faustino)
