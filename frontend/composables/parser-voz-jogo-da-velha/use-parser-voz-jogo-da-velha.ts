interface IObjetoParserVozJogada {
  linha: number;
  coluna: number;
}
interface IErroParserVozJogada {
  mensagem: string;
}

type ParserVozJogadaErro ={
      sucesso: false;
      erro: IErroParserVozJogada;
};

type ParserVozJogadaSucesso = {
  sucesso: true;
  objeto: IObjetoParserVozJogada;
};

type ParserVozJogada = ParserVozJogadaSucesso | ParserVozJogadaErro;

const palavrasChaves = Object.freeze({
  jogar: "jogar",
  linha: "linha",
  coluna: "coluna",
});

const mensagemErro = (
  mensagem: string
): {
  sucesso: false;
  erro: IErroParserVozJogada;
} => {
  return {
    sucesso: false,
    erro: {
      mensagem,
    },
  };
};

const obterValorIndex = (textosSepadados: string[], texto: string) => {
  const index = textosSepadados.indexOf(texto);
  if (index == -1) {
    return mensagemErro("Comando invalido");
  }

  const valor = textosSepadados[index + 1];
  if (!valor) {
    return mensagemErro("Comando invalido");
  }

  const valorInteriro = parseInt(valor);
  if (isNaN(valorInteriro)) return mensagemErro("Comando invalido");
  if(valorInteriro>4 || valorInteriro<1) return mensagemErro("Comando invalido");
  return valorInteriro;
};

export const useParserVozJogada = (texto: string): ParserVozJogada => {
  const incioTextoComandoJogada = texto
    .substring(texto.indexOf(palavrasChaves.jogar))
    .toLowerCase();

  if (!incioTextoComandoJogada) {
    return mensagemErro("Comando invalido");
  }

  const textosSepadados = incioTextoComandoJogada.split(" ");

  if (textosSepadados.length != 5) return mensagemErro("Comando invalido");

  const valorLinha = obterValorIndex(textosSepadados, palavrasChaves.linha);
  if (typeof valorLinha === "object") return valorLinha;

  const valorColuna = obterValorIndex(textosSepadados, palavrasChaves.coluna);
  if (typeof valorColuna === "object") return valorColuna;

  return {
    sucesso: true,
    objeto: {
      linha: valorLinha,
      coluna: valorColuna,
    },
  };
};
