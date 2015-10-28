# OneCog.Io.Plex
Portable client library for accessing Plex Media Server content library

[![OneCog.Io.Plex Build Status](https://www.myget.org/BuildSource/Badge/onecog?identifier=d44960cd-ebaa-41cd-9e30-3e0cb3743399)](https://www.myget.org/)

## Usage
Install nuget package from the OneCog package repository:

```Install-Package OneCog.Io.Plex -Source https://www.myget.org/F/onecog/api/v2/package```

Use the following code to instantiate a server:

```OneCog.Io.Plex.IServer server = OneCog.Io.Plex.Server.Create("[Plex Host Name or IP]", [Plex Port]);```

Retrieve all artists with:

```IList<OneCog.Io.Plex.Music.Models.IArtist> artists = await server.Music.Artists.All.ToList();```

Retrieve all albums with:

```IList<OneCog.Io.Plex.Music.Models.IAlbum> albumss = await server.Music.Albums.All.ToList();```

