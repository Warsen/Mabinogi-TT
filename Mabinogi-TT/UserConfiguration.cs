using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MabinogiTT
{
	internal class UserConfiguration : INotifyPropertyChanged
	{
		private TimeSpan _serverTimeOffset;

		public TimeSpan ServerTimeOffset
		{
			get => _serverTimeOffset;
			set
			{
				if (_serverTimeOffset != value)
				{
					_serverTimeOffset = value;
					OnPropertyChanged();
				}
			}
		}

		public void SaveConfiguration(StreamWriter writer)
		{
			writer.Write(_serverTimeOffset.Ticks);
		}

		public void LoadConfiguration(StreamReader reader)
		{
			string line = reader.ReadLine()!;
			_serverTimeOffset = new TimeSpan(Int64.Parse(line));
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
