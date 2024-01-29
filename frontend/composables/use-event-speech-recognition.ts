type FnString = (texto: string) => void;

export type EventSpeechRecognition = {
  adicionarEventos(ouvinte: FnString): void;
  iniciar(): void;
  distruir(): void;
  possuiSuporte: boolean;
};

export const useEventSpeechRecognition = (): EventSpeechRecognition => {
  if (
    !("SpeechRecognition" in window) &&
    !("webkitSpeechRecognition" in window)
  ) {
    return {
      adicionarEventos: (_: FnString) => {
        console.error("Nao possui suporte");
      },
      iniciar: () => {
        console.error("Nao possui suporte");
      },
      distruir: () => {
        console.error("Nao possui suporte");
      },
      possuiSuporte: false,
    };
  }
console.log("useEventSpeechRecognition");
  const disparadorEvento = {
    ouvintes: [] as FnString[],
    dispararEventos(texto: string) {
      this.ouvintes
        .filter((ouvinte) => typeof ouvinte === "function")
        .forEach((ouvinte) => ouvinte(texto));
    },
  };

  var recognition: SpeechRecognition | undefined = undefined;

  const iniciarReconhecimento = () => {
    recognition = new webkitSpeechRecognition();
    recognition.interimResults = true;
    recognition.lang = "pt-BR";
    recognition.continuous = true;

    recognition.onresult = function (event) {
      for (let i = event.resultIndex; i < event.results.length; i++) {
        if (event.results[i].isFinal) {
          const content = event.results[i][0].transcript.trim();
          disparadorEvento.dispararEventos(content);
        }
      }
    };

    recognition.start();
  };

  return {
    possuiSuporte: true,
    iniciar() {
      iniciarReconhecimento();
    },
    adicionarEventos(ouvinte: FnString) {
      disparadorEvento.ouvintes.push(ouvinte);
    },
    distruir() {      
      disparadorEvento.ouvintes = [];

      if (!recognition) return;

      recognition.stop();
      recognition.abort();
      recognition = undefined;
    }
  };
};
