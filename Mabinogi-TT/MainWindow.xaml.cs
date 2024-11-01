using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading; // For DispatcherTimer

namespace MabinogiTT
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private UserConfiguration _userConfiguration;
		private static readonly TimeSpan _carasekCyclePeriod = new(0, 6, 0); // Cobh <-> Belvast
		private static readonly TimeSpan _carasekBoardingDuration = new(0, 3, 30);
		private static readonly TimeSpan _carasekSailingDuration = new(0, 2, 30);
		private static readonly TimeSpan _lindenCyclePeriod = new(0, 11, 0); // Cobh <-> Qilla
		private static readonly TimeSpan _lindenBoardingDuration = new(0, 6, 0);
		private static readonly TimeSpan _lindenSailingDuration = new(0, 5, 0);
		private static readonly TimeSpan _igernaCyclePeriod = new(0, 11, 0); // Ceann <-> Sella
		private static readonly TimeSpan _igernaBoardingDuration = new(0, 6, 0);
		private static readonly TimeSpan _igernaSailingDuration = new(0, 5, 0);
		private static readonly TimeSpan _caoimhimCyclePeriod = new(0, 11, 0); // Ceann <-> Connous
		private static readonly TimeSpan _caoimhimBoardingDuration = new(0, 6, 0);
		private static readonly TimeSpan _caoimhimSailingDuration = new(0, 5, 0);
		private static readonly TimeSpan _cobhToBelvastStartOffset = new(0, 2, 30);
		private static readonly TimeSpan _belvastToCobhStartOffset = new(0, 2, 0);
		private static readonly TimeSpan _cobhToQillaStartOffset = new(0, 1, 30);
		private static readonly TimeSpan _qillaToCobhStartOffset = new(0, 1, 0);
		private static readonly TimeSpan _ceannToSellaStartOffset = new(0, 1, 30);
		private static readonly TimeSpan _sellaToCeannStartOffset = new(0, 0, 30);
		private static readonly TimeSpan _ceannToConnousStartOffset = new(0, 1, 00);
		private static readonly TimeSpan _connousToCeannStartOffset = new(0, 0, 30);
		private static readonly Color _colorBoarding = Colors.Green;
		private static readonly Color _colorBoarding2 = Colors.Yellow;
		private static readonly Color _colorBoardingBg = Color.FromRgb((byte)(_colorBoarding.R / 2), (byte)(_colorBoarding.G / 2), (byte)(_colorBoarding.B / 2));
		private static readonly Color _colorSailing = Colors.Maroon;
		private static readonly Color _colorSailing2 = Colors.Yellow;
		private static readonly Color _colorSailingBg = Color.FromRgb((byte)(_colorSailing.R / 2), (byte)(_colorSailing.G / 2), (byte)(_colorSailing.B / 2));

		private readonly DispatcherTimer _dispatcherTimer = new(DispatcherPriority.Normal);
		private readonly SolidColorBrush _cobhToBelvastFg = new(Colors.Transparent);
		private readonly SolidColorBrush _cobhToBelvastBg = new(Colors.Transparent);
		private readonly SolidColorBrush _belvastToCobhFg = new(Colors.Transparent);
		private readonly SolidColorBrush _belvastToCobhBg = new(Colors.Transparent);
		private readonly SolidColorBrush _cobhToQillaFg = new(Colors.Transparent);
		private readonly SolidColorBrush _cobhToQillaBg = new(Colors.Transparent);
		private readonly SolidColorBrush _qillaToCobhFg = new(Colors.Transparent);
		private readonly SolidColorBrush _qillaToCobhBg = new(Colors.Transparent);
		private readonly SolidColorBrush _ceannToSellaFg = new(Colors.Transparent);
		private readonly SolidColorBrush _ceannToSellaBg = new(Colors.Transparent);
		private readonly SolidColorBrush _sellaToCeannFg = new(Colors.Transparent);
		private readonly SolidColorBrush _sellaToCeannBg = new(Colors.Transparent);
		private readonly SolidColorBrush _ceannToConnousFg = new(Colors.Transparent);
		private readonly SolidColorBrush _ceannToConnousBg = new(Colors.Transparent);
		private readonly SolidColorBrush _connousToCeannFg = new(Colors.Transparent);
		private readonly SolidColorBrush _connousToCeannBg = new(Colors.Transparent);
		private BoatState _cobhToBelvastBoatState = BoatState.Boarding;
		private BoatState _belvastToCobhBoatState = BoatState.Boarding;
		private BoatState _cobhToQillaBoatState = BoatState.Boarding;
		private BoatState _qillaToCobhBoatState = BoatState.Boarding;
		private BoatState _ceannToSellaBoatState = BoatState.Boarding;
		private BoatState _sellaToCeannBoatState = BoatState.Boarding;
		private BoatState _ceannToConnousBoatState = BoatState.Boarding;
		private BoatState _connousToCeannBoatState = BoatState.Boarding;

		internal UserConfiguration UserConfiguration
		{
			get => _userConfiguration;
			set => _userConfiguration = value;
		}

		internal MainWindow(UserConfiguration userConfiguration)
		{
			InitializeComponent();

			_userConfiguration = userConfiguration;

			DateTime now = DateTime.Now;
			DateTime nowA = now.AddSeconds(2);
			TimeSpan delay = new DateTime(nowA.Year, nowA.Month, nowA.Day, nowA.Hour, nowA.Minute, nowA.Second) - now;
			_dispatcherTimer.Interval = delay;
			_dispatcherTimer.Tick += InitialTick;
			_dispatcherTimer.Start();

			_cobhToBelvastProgress.Foreground = _cobhToBelvastFg;
			_cobhToBelvastProgress.Background = _cobhToBelvastBg;
			_cobhToBelvastProgress.BorderBrush = _cobhToBelvastBg;
			_belvastToCobhProgress.Foreground = _belvastToCobhFg;
			_belvastToCobhProgress.Background = _belvastToCobhBg;
			_belvastToCobhProgress.BorderBrush = _belvastToCobhBg;
			_cobhToQillaProgress.Foreground = _cobhToQillaFg;
			_cobhToQillaProgress.Background = _cobhToQillaBg;
			_cobhToQillaProgress.BorderBrush = _cobhToQillaBg;
			_qillaToCobhProgress.Foreground = _qillaToCobhFg;
			_qillaToCobhProgress.Background = _qillaToCobhBg;
			_qillaToCobhProgress.BorderBrush = _qillaToCobhBg;
			_ceannToSellaProgress.Foreground = _ceannToSellaFg;
			_ceannToSellaProgress.Background = _ceannToSellaBg;
			_ceannToSellaProgress.BorderBrush = _ceannToSellaBg;
			_sellaToCeannProgress.Foreground = _sellaToCeannFg;
			_sellaToCeannProgress.Background = _sellaToCeannBg;
			_sellaToCeannProgress.BorderBrush = _sellaToCeannBg;
			_ceannToConnousProgress.Foreground = _ceannToConnousFg;
			_ceannToConnousProgress.Background = _ceannToConnousBg;
			_ceannToConnousProgress.BorderBrush = _ceannToConnousBg;
			_connousToCeannProgress.Foreground = _connousToCeannFg;
			_connousToCeannProgress.Background = _connousToCeannBg;
			_connousToCeannProgress.BorderBrush = _connousToCeannBg;
		}

		private void InitialTick(object? sender, EventArgs e)
		{
			_dispatcherTimer.Stop();
			_dispatcherTimer.Tick -= InitialTick;
			_dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
			_dispatcherTimer.Tick += TimerTick;
			_dispatcherTimer.Start();

			_serverTimeSecondsSlider.Value = _userConfiguration.ServerTimeOffset.Seconds;
			_serverTimeOffsetText.Text = _userConfiguration.ServerTimeOffset.ToString(@"h\:mm\:ss");
			InitializeAllTimers(GetTimeSinceTuesday7AM(DateTime.Now));
		}

		private static TimeSpan GetTimeSinceTuesday7AM(DateTime dateTime)
		{
			TimeSpan serverUptime;
			if (dateTime.DayOfWeek == DayOfWeek.Thursday && dateTime.Hour >= 7)
			{
				serverUptime = dateTime - new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 7, 0, 0);
				serverUptime = new TimeSpan(serverUptime.Days, serverUptime.Hours, serverUptime.Minutes, serverUptime.Seconds);
				return serverUptime;
			}
			else
			{
				serverUptime = dateTime - dateTime.Date.AddDays(-((dateTime.DayOfWeek - DayOfWeek.Thursday + 7) % 7)).AddHours(7);
				serverUptime = new TimeSpan(serverUptime.Days, serverUptime.Hours, serverUptime.Minutes, serverUptime.Seconds);
				return serverUptime;
			}
		}

		private void InitializeAllTimers(TimeSpan timeSince)
		{
			InitializeTimer(_carasekCyclePeriod, _belvastToCobhStartOffset, _carasekBoardingDuration, _carasekSailingDuration,
				timeSince, ref _belvastToCobhBoatState, _belvastToCobhProgress, _belvastToCobhFg, _belvastToCobhBg, _belvastToCobhTimerText);
			InitializeTimer(_carasekCyclePeriod, _cobhToBelvastStartOffset, _carasekBoardingDuration, _carasekSailingDuration,
				timeSince, ref _cobhToBelvastBoatState, _cobhToBelvastProgress, _cobhToBelvastFg, _cobhToBelvastBg, _cobhToBelvastTimerText);
			InitializeTimer(_lindenCyclePeriod, _cobhToQillaStartOffset, _lindenBoardingDuration, _lindenSailingDuration,
				timeSince, ref _cobhToQillaBoatState, _cobhToQillaProgress, _cobhToQillaFg, _cobhToQillaBg, _cobhToQillaTimerText);
			InitializeTimer(_lindenCyclePeriod, _qillaToCobhStartOffset, _lindenBoardingDuration, _lindenSailingDuration,
				timeSince, ref _qillaToCobhBoatState, _qillaToCobhProgress, _qillaToCobhFg, _qillaToCobhBg, _qillaToCobhTimerText);
			InitializeTimer(_igernaCyclePeriod, _ceannToSellaStartOffset, _igernaBoardingDuration, _igernaSailingDuration,
				timeSince, ref _ceannToSellaBoatState, _ceannToSellaProgress, _ceannToSellaFg, _ceannToSellaBg, _ceannToSellaTimerText);
			InitializeTimer(_igernaCyclePeriod, _sellaToCeannStartOffset, _igernaBoardingDuration, _igernaSailingDuration,
				timeSince, ref _sellaToCeannBoatState, _sellaToCeannProgress, _sellaToCeannFg, _sellaToCeannBg, _sellaToCeannTimerText);
			InitializeTimer(_caoimhimCyclePeriod, _ceannToConnousStartOffset, _caoimhimBoardingDuration, _caoimhimSailingDuration,
				timeSince, ref _ceannToConnousBoatState, _ceannToConnousProgress, _ceannToConnousFg, _ceannToConnousBg, _ceannToConnousTimerText);
			InitializeTimer(_caoimhimCyclePeriod, _connousToCeannStartOffset, _caoimhimBoardingDuration, _caoimhimSailingDuration,
				timeSince, ref _connousToCeannBoatState, _connousToCeannProgress, _connousToCeannFg, _connousToCeannBg, _connousToCeannTimerText);
		}

		private void InitializeTimer(TimeSpan cyclePeriod, TimeSpan startOffset, TimeSpan boardingDuration, TimeSpan sailingDuration,
			TimeSpan now, ref BoatState boatState, ProgressBar progress, SolidColorBrush fg, SolidColorBrush bg, TextBlock timerTextBlock)
		{
			TimeSpan nextCycle = new(cyclePeriod.Ticks - ((now.Ticks + startOffset.Ticks + _userConfiguration.ServerTimeOffset.Ticks) % cyclePeriod.Ticks));
			if (nextCycle >= sailingDuration)
			{
				boatState = BoatState.Boarding;
				progress.Maximum = boardingDuration.TotalSeconds;
				progress.Value = nextCycle.TotalSeconds - sailingDuration.TotalSeconds;
				fg.Color = _colorBoarding;
				bg.Color = _colorBoardingBg;
			}
			else
			{
				boatState = BoatState.Sailing;
				progress.Maximum = sailingDuration.TotalSeconds;
				progress.Value = nextCycle.TotalSeconds;
				fg.Color = _colorSailing;
				bg.Color = _colorSailingBg;
			}
			timerTextBlock.Text = TimeSpan.FromSeconds(progress.Value).ToString(@"m\:ss");
		}

		private void TimerTick(object? sender, EventArgs e)
		{
			UpdateTimer(_carasekBoardingDuration, _carasekSailingDuration,
				ref _belvastToCobhBoatState, _belvastToCobhProgress, _belvastToCobhFg, _belvastToCobhBg, _belvastToCobhTimerText);
			UpdateTimer(_carasekBoardingDuration, _carasekSailingDuration,
				ref _cobhToBelvastBoatState, _cobhToBelvastProgress, _cobhToBelvastFg, _cobhToBelvastBg, _cobhToBelvastTimerText);
			UpdateTimer(_lindenBoardingDuration, _lindenSailingDuration,
				ref _cobhToQillaBoatState, _cobhToQillaProgress, _cobhToQillaFg, _cobhToQillaBg, _cobhToQillaTimerText);
			UpdateTimer(_lindenBoardingDuration, _lindenSailingDuration,
				ref _qillaToCobhBoatState, _qillaToCobhProgress, _qillaToCobhFg, _qillaToCobhBg, _qillaToCobhTimerText);
			UpdateTimer(_igernaBoardingDuration, _igernaSailingDuration,
				ref _ceannToSellaBoatState, _ceannToSellaProgress, _ceannToSellaFg, _ceannToSellaBg, _ceannToSellaTimerText);
			UpdateTimer(_igernaBoardingDuration, _igernaSailingDuration,
				ref _sellaToCeannBoatState, _sellaToCeannProgress, _sellaToCeannFg, _sellaToCeannBg, _sellaToCeannTimerText);
			UpdateTimer(_caoimhimBoardingDuration, _caoimhimSailingDuration,
				ref _ceannToConnousBoatState, _ceannToConnousProgress, _ceannToConnousFg, _ceannToConnousBg, _ceannToConnousTimerText);
			UpdateTimer(_caoimhimBoardingDuration, _caoimhimSailingDuration,
				ref _connousToCeannBoatState, _connousToCeannProgress, _connousToCeannFg, _connousToCeannBg, _connousToCeannTimerText);
		}

		private void UpdateTimer(TimeSpan boardingDuration, TimeSpan sailingDuration,
			ref BoatState boatState, ProgressBar progress, SolidColorBrush fg, SolidColorBrush bg, TextBlock timerTextBlock)
		{
			if (progress.Value > 0)
			{
				progress.Value--;
				if (boatState == BoatState.Boarding)
				{
					if (progress.Value < 30)
					{
						fg.Color = InterpolateColors(_colorBoarding2, _colorBoarding, progress.Value / 30.0);
					}
				}
			}
			else if (progress.Value == 0)
			{
				if (boatState == BoatState.Boarding)
				{
					boatState = BoatState.Sailing;
					progress.Maximum = sailingDuration.TotalSeconds;
					progress.Value = progress.Maximum - 1;
					fg.Color = _colorSailing;
					bg.Color = _colorSailingBg;
				}
				else
				{
					boatState = BoatState.Boarding;
					progress.Maximum = boardingDuration.TotalSeconds;
					progress.Value = progress.Maximum - 1;
					fg.Color = _colorBoarding;
					bg.Color = _colorBoardingBg;
				}
			}
			timerTextBlock.Text = TimeSpan.FromSeconds(progress.Value).ToString(@"m\:ss");
		}

		private static Color InterpolateColors(Color startColor, Color endColor, double interpolationFactor)
		{
			byte r = (byte)(startColor.R + (endColor.R - startColor.R) * interpolationFactor);
			byte g = (byte)(startColor.G + (endColor.G - startColor.G) * interpolationFactor);
			byte b = (byte)(startColor.B + (endColor.B - startColor.B) * interpolationFactor);
			byte a = (byte)(startColor.A + (endColor.A - startColor.A) * interpolationFactor);

			return Color.FromArgb(a, r, g, b);
		}

		private void OnServerTimeSliderChange(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			_userConfiguration.ServerTimeOffset = new TimeSpan(_userConfiguration.ServerTimeOffset.Hours, _userConfiguration.ServerTimeOffset.Minutes, (int)_serverTimeSecondsSlider.Value);

			_serverTimeOffsetText.Text = _userConfiguration.ServerTimeOffset.ToString(@"h\:mm\:ss");
			InitializeAllTimers(GetTimeSinceTuesday7AM(DateTime.Now));
		}

		private void OnServerTimeMinutesMinusClick(object sender, RoutedEventArgs e)
		{
			if (_userConfiguration.ServerTimeOffset.TotalMinutes >= 1.0)
				_userConfiguration.ServerTimeOffset -= new TimeSpan(0, 1, 0);

			_serverTimeOffsetText.Text = _userConfiguration.ServerTimeOffset.ToString(@"h\:mm\:ss");
			InitializeAllTimers(GetTimeSinceTuesday7AM(DateTime.Now));
		}

		private void OnServerTimeMinutesPlusClick(object sender, RoutedEventArgs e)
		{
			_userConfiguration.ServerTimeOffset += new TimeSpan(0, 1, 0);

			_serverTimeOffsetText.Text = _userConfiguration.ServerTimeOffset.ToString(@"h\:mm\:ss");
			InitializeAllTimers(GetTimeSinceTuesday7AM(DateTime.Now));
		}

		private void OnServerTimeHoursMinusClick(object sender, RoutedEventArgs e)
		{
			if (_userConfiguration.ServerTimeOffset.TotalHours >= 1.0)
				_userConfiguration.ServerTimeOffset -= new TimeSpan(1, 0, 0);

			_serverTimeOffsetText.Text = _userConfiguration.ServerTimeOffset.ToString(@"h\:mm\:ss");
			InitializeAllTimers(GetTimeSinceTuesday7AM(DateTime.Now));
		}

		private void OnServerTimeHoursPlusClick(object sender, RoutedEventArgs e)
		{
			_userConfiguration.ServerTimeOffset += new TimeSpan(1, 0, 0);

			_serverTimeOffsetText.Text = _userConfiguration.ServerTimeOffset.ToString(@"h\:mm\:ss");
			InitializeAllTimers(GetTimeSinceTuesday7AM(DateTime.Now));
		}
	}
}
