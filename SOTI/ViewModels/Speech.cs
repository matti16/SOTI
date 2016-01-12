using System;
using System.Globalization;
using System.Speech.Synthesis;

namespace SOTI.ViewModels
{
    public class Speech
    {
        private SpeechSynthesizer sp = new SpeechSynthesizer();

        public void Say(string text)
        {
            this.Say(text, 100, 0);
        }

        public void Say(string text, int volume, int rate)
        {
            //foreach (InstalledVoice voice in sp.GetInstalledVoices())
            //{
            //    VoiceInfo info = voice.VoiceInfo;

                //Console.WriteLine(" Name:          " + info.Name);
                //Console.WriteLine(" Culture:       " + info.Culture);
                //Console.WriteLine(" Age:           " + info.Age);
                //Console.WriteLine(" Gender:        " + info.Gender);
                //Console.WriteLine(" Description:   " + info.Description);
                //Console.WriteLine(" ID:            " + info.Id);
            //}

            if (volume >= 0 && volume <= 100)
                sp.Volume = volume;
            else
                sp.Volume = 100;

            // rappresenta la velocità di lettura
            if (rate >= -10 && rate <= 10)
                sp.Rate = rate;
            else
                sp.Rate = 0;

            //CultureInfo culture = CultureInfo.CreateSpecificCulture("it-IT");
            //spSynth.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Teen, 0, culture);
            //spSynth.SelectVoice("ScanSoft Silvia_Dri40_16kHz");
            //spSynth.SelectVoice("Microsoft Elsa Desktop");
            //spSynth.SelectVoice("Paola");
            //spSynth.SelectVoice("Luca");
            //spSynth.SelectVoice("Roberto");

            PromptBuilder builder = new PromptBuilder();

            builder.StartVoice("Microsoft Elsa Desktop");
            builder.StartSentence();

            builder.StartStyle(new PromptStyle() { Emphasis = PromptEmphasis.Strong, Rate = PromptRate.Medium });
            string high = "<prosody pitch=\"x-high\"> " + text + " </prosody >";
            builder.AppendSsmlMarkup(high);
            builder.EndStyle();
            builder.EndSentence();
            builder.EndVoice();

            // Asynchronous
            sp.SpeakAsync(builder);
        }

        public void Stop()
        {
            if (sp.State == SynthesizerState.Speaking)
                sp.SpeakAsyncCancelAll();
        }
    }
}
