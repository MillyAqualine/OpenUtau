﻿using OpenUtau.Api;
using OpenUtau.Core.Ustx;

namespace OpenUtau.Core {
    /// <summary>
    /// The simplest Phonemizer possible. Simply pass the lyric as phoneme.
    /// </summary>
    [Phonemizer("Default Phonemizer", "DEFAULT")]
    public class DefaultPhonemizer : Phonemizer {
        private USinger singer;
        public override void SetSinger(USinger singer) => this.singer = singer;
        public override Result Process(Note[] notes, Note? prev, Note? next, Note? prevNeighbour, Note? nextNeighbour, Note[] prevNeighbours) {
            // Note that even when input has multiple notes, only the leading note is used to produce phoneme.
            // This is because the 2nd+ notes will always be extender notes, i.e., with lyric "+" or "+<number>".
            // For this simple phonemizer, all these notes maps to a single phoneme.
            string alias = notes[0].lyric;
            if (singer.TryGetMappedOto(notes[0].lyric, notes[0].tone, out var oto)) {
                alias = oto.Alias;
            }
            return new Result {
                phonemes = new Phoneme[] {
                    new Phoneme {
                        phoneme = alias,
                    }
                }
            };
        }
    }
}
