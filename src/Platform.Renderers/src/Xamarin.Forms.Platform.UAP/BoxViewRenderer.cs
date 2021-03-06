﻿using System;
using System.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation.Peers;
using WShape = Microsoft.UI.Xaml.Shapes.Shape;

namespace Xamarin.Forms.Platform.UWP
{
	[Obsolete("BoxViewRenderer is obsolete as of version 4.6.0. Please use BoxViewBorderRenderer instead.", false)]
	public class BoxViewRenderer : ViewRenderer<BoxView, Microsoft.UI.Xaml.Shapes.Rectangle>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				if (Control == null)
				{
					var rect = new Microsoft.UI.Xaml.Shapes.Rectangle
					{
						DataContext = Element
					};

					rect.SetBinding(WShape.FillProperty, new Microsoft.UI.Xaml.Data.Binding { Converter = new BrushConverter(), ConverterParameter = e.NewElement.Color, Path = new PropertyPath("Background") });

					SetNativeControl(rect);
				}

				SetCornerRadius(Element.CornerRadius);
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == BoxView.CornerRadiusProperty.PropertyName)
				SetCornerRadius(Element.CornerRadius);
		}

		protected override AutomationPeer OnCreateAutomationPeer()
		{
			// We need an automation peer so we can interact with this in automated tests
			if (Control == null)
			{
				return new FrameworkElementAutomationPeer(this);
			}

			return new FrameworkElementAutomationPeer(Control);
		}

		private void SetCornerRadius(CornerRadius cornerRadius)
		{
			Control.RadiusX = cornerRadius.TopLeft;
			Control.RadiusY = cornerRadius.BottomRight;
		}
	}
}
