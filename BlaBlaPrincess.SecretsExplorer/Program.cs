using System;
using BlaBlaPrincess.SecretsExplorer.Business;
using CommandLine;
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable ClassNeverInstantiated.Local

namespace BlaBlaPrincess.SecretsExplorer
{
    internal static class Program
    {
        private class Options
        {
            [Option("src", Default = "./src/Utrom's secrets/")]
            public string Source { get; set; }
            
            [Option("dest", Default = "./dest/")]
            public string Destination { get; set; }
            
            [Option("zip", Default = false)]
            public bool Zip { get; set; }
        }
        
        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(options =>
            {
                try
                {
                    var explorer = new UtromsSecretsExplorer();
                    explorer.ExploreSecrets(options.Source, options.Destination, options.Zip);
                    Console.WriteLine(explorer.GetInfo());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }
    }
}

/*              ,;;;!!;;
        ,;<!!!!!!!!!!!;
     `'<!!!!!!!!!!(``'!!
           ,;;;;;;, `\. `\         .,c$$$$$$$$$$$$$ec,.
      ,;;!!!!!!!!!!!>; `. ,;!>> .e$$$$$$$$"".  "?$$$$$$$e.
 <:<!!!!!!!!'` ..,,,.`` ,!!!' ;,(?""""""";!!''<; `?$$$$$$PF ,;,
  `'!!!!;;;;;;;;<!'''`  !!! ;,`'``''!!!;!!!!`..`!;  ,,,  .<!''`).
     ```'''''``         `!  `!!!!><;;;!!!!! J$$b,`!>;!!:!!`,d?b`!>
                          `'-;,(<!!!!!!!!!> $F   )...:!.  d"  3 !>
                              ```````''<!!!- "=-='     .  `--=",!>
                         .ze$$$$$$$$$er  .,cd$$$$$$$$$$$$$$$$bc.'
                      .e$$$$$$$$$$$$$$,$$$$$$$$$$$$$$$$$$$$$$$$$$.
                     z$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$c .
                    J$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$c
                    $$$$$$$$$$$$$$P"`?$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$b
                    $$$$$$$$$$$$$$ dbc `""?$$$$$$$$$$$$$$$$$$$$$$?$$$$$$$c
                    ?$$$$$$$$$$$$$$$$$$c.      """"????????"""" c$$$$$$$$P
         .,,.        "$$$$$$$$$$$$$$$$$$$$c.   ._              J$$$$$$$$$
 .,,cc$$$$$$$$$bec,.  `?$$$$$$$$$$$$$$$$$$$$$c.```%%%%,%%%,   c$$$$$$$$P"
$$$$$$$$$$$$$$$$$$$$$$c  ""?$$$$$$$$$$$$$$$$$$$$$bc,,.`` .,,c$$$$$$$P"",cb
$$$$$$$$$$$$$$$$$$$$$$$b bc,.""??$$$$$$$$$$$$$$FF""?????"",J$$$$$P" ,zd$$$
$$$$$$$$$$$$$$$$$$$$$$$$ ?$???%   `""??$$$$$$$$$$$$bcucd$$$P"""  ==$$$$$$$
$$$$$$$$$$$$$$$$$$$$$$$P" ,;;;<!!!!!>;;,. `""""??????""  ,;;;;;;;;;, `"?$$
$$$$$$$$$$$$$$$$$$$P"",;!!!!!!!!!!!!!!!!!!!!!!!;;;;;;!!!!!!!!!!!!!!!!!;  "
$$$$$$$$$$$$$$$$$"",;!!!!!!'``.,,,,,.```''!!!!!!!!!!!!!!!!!!!!'''''!!!!!>
$$$$$$$$$$$$$$$" ;!!!!!'`.z$$$$$$$$$$$$$ec,. ```'''''''``` .,,ccecec,`'!!!
$$$$$$$$$$$$$" ;!!!!' .c$$$$$$$$$$$$$$$$$$$$$$$c  :: .c$$$$$$$$$$$$$$$. <!
$$$$$$$$$$$" ;!!!!' .d$$$$$$$$$$$$$$$$$$$$$$$$$$b ' z$$$$$$$$$$$$$$$$$$c <
$$$$$$$$$F  <!!!'.c$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$b  $$$$$$$$$$$$$$$$$$$$r
$$$$$$$P" <!!!' c$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$, "$$$$$$$$$$$$$$$$$$$$
$$$$$P" ;!!!' z$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$  $$$$$$$$$$$$$$$$$$$$
 */