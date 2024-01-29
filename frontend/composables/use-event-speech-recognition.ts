type Fn = (texto:string)=> void

export const useEventSpeechRecognition = () => {
  const recognition = new webkitSpeechRecognition();
  recognition.interimResults = true;
  recognition.lang = "pt-BR";
  recognition.continuous = true;

  const disparadorEvento = {
    ouvintes: [] as Fn[],
    dispararEventos(texto:string) { 
      this.ouvintes
        .filter((ouvinte) => typeof ouvinte === "function")
        .forEach((ouvinte) => ouvinte(texto));
    }
  };

  recognition.onresult = function (event) {
    for (let i = event.resultIndex; i < event.results.length; i++) {
      if (event.results[i].isFinal) {
        const content = event.results[i][0].transcript.trim();
        disparadorEvento.dispararEventos(content);
      }
    }
  };
  recognition.start();

  return {
    adicionarEventos(ouvinte: Fn) {
      disparadorEvento.ouvintes.push(ouvinte);
    }
  }
};