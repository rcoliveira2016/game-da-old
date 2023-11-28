export interface ColJogoDaVelha {
    selecionado?: "X"|"O"
}
export interface ColSelecionadoEvent {
    col: ColJogoDaVelha;
    indexCol: number;
    indexRow: number;
}