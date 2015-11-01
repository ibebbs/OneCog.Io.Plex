using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCog.Io.Plex.Transcoder
{
    public interface IProvider
    {
        Uri Track(Music.ITrack track);
    }

    internal class Provider : IProvider
    {
        private readonly Api.IInstance _api;

        public Provider(Api.IInstance api)
        {
            _api = api;
        }

        public Uri Track(Music.ITrack track)
        {
            return new Uri(_api.Host, string.Format("audio/:/transcode/universal/start.m3u8?maxVideoBitrate=720&amp;protocol=hls&amp;session=59bacd74-a443-4340-905e-762e04821a0d&amp;offset=0&amp;videoQuality=100&amp;videoResolution=576x320&amp;directStream=1&amp;directPlay=0&amp;audioBoost=0&amp;fastSeek=1&amp;subtitleSize=100&amp;path={0}", track.Key));
        }
    }
}
