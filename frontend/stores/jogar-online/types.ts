import type { ValorColSelecionado } from "~/types/jogo/jogo-da-velha";

export interface JogoDaVelhaHubInputModel {
  Identificador: string;
  IndexLinha: number;
  IndexColuna: number;
}

export interface JogadaSetadaOutputModel {
  tabuleiro: ValorColSelecionado[][];
  proximoJogador: ValorColSelecionado;
  vencedor?: ValorColSelecionado;
}