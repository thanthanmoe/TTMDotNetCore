(function($) {
    /* "use strict" */


 var dzChartlist = function(){
	//let draw = Chart.controllers.line.__super__.draw; //draw shadow
	var screenWidth = $(window).width();
	
	var chartBar = function(){
		
		var options = {
			  series: [
				{
					name: 'Running',
					data: [50, 18, 70, 40, 90, 70, 20],
					//radius: 12,	
				}, 
				{
				  name: 'Cycling',
				  data: [80, 40, 55, 20, 45, 30, 80]
				}, 
				
			],
				chart: {
				type: 'bar',
				height: 370,
				
				toolbar: {
					show: false,
				},
				
			},
			plotOptions: {
			  bar: {
				horizontal: false,
				columnWidth: '57%',
				//endingShape: "rounded",
				  borderRadius: 10,
			  },
			  
			},
			states: {
			  hover: {
				filter: 'none',
			  }
			},
			colors:['#D2D2D2', '#EBEBEB'],
			dataLabels: {
			  enabled: false,
			},
			markers: {
		shape: "circle",
		},
		
		
			legend: {
				show: false,
				fontSize: '12px',
				labels: {
					colors: '#000000',
					
					},
				markers: {
				width: 18,
				height: 18,
				strokeWidth: 0,
				strokeColor: '#fff',
				fillColors: undefined,
				radius: 12,	
				}
			},
			stroke: {
			  show: true,
			  width: 4,
			  colors: ['transparent']
			},
			grid: {
				borderColor: '#eee',
			},
			xaxis: {
				
			  categories: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
			  labels: {
			   style: {
				  colors: '#787878',
				  fontSize: '13px',
				  fontFamily: 'poppins',
				  fontWeight: 100,
				  cssClass: 'apexcharts-xaxis-label',
				},
			  },
			  crosshairs: {
			  show: false,
			  }
			},
			yaxis: {
				labels: {
					offsetX:-16,
				   style: {
					  colors: '#787878',
					  fontSize: '13px',
					   fontFamily: 'poppins',
					  fontWeight: 100,
					  cssClass: 'apexcharts-xaxis-label',
				  },
			  },
			},
			fill: {
			  opacity: 1,
			  colors:['#D2D2D2', '#EBEBEB'],
			},
			tooltip: {
			  y: {
				formatter: function (val) {
				  return "$ " + val + " thousands"
				}
			  }
			},
			};

			var chartBar1 = new ApexCharts(document.querySelector("#chartBar"), options);
			chartBar1.render();
	}	
	var widgetChart1 = function(){
		if(jQuery('#widgetChart1').length > 0 ){
			const chart_widget_1 = document.getElementById("widgetChart1").getContext('2d');

			new Chart(chart_widget_1, {
				type: "line",
				data: {
					labels: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "January", "February", "March", "April"],

					datasets: [{
						label: "Sales Stats",
						backgroundColor: ['rgba(234, 73, 137, 0)'],
						borderColor: '#1EAAE7',
						pointBackgroundColor: '#1EAAE7',
						pointBorderColor: '#1EAAE7',
						borderWidth:4,
						borderRadius:10,
						pointHoverBackgroundColor: '#1EAAE7',
						pointHoverBorderColor: '#1EAAE7',
						tension:0.5,
						
						data: [12, 13, 10, 18, 14, 24, 16, 12, 19, 21, 16, 14, 24, 21, 13, 15, 27, 29, 21, 11, 14, 19, 21, 17]
					}]
				},
				options: {
					title: {
						display: !1
					},
					
					
					plugins:{
						legend:false,
						tooltip: {
							intersect: !1,
							mode: "nearest",
							xPadding: 10,
							yPadding: 10,
							caretPadding: 10
						},
						
					},
					responsive: !0,
					maintainAspectRatio: !1,
					hover: {
						mode: "index"
					},
					scales: {
						x:{
							display: !1,
							gridLines: !1,
							scaleLabel: {
								display: !0,
								labelString: "Month"
							}
						},
						y: {
							display: !1,
							gridLines: !1,
							scaleLabel: {
								display: !0,
								labelString: "Value"
							},
							ticks: {
								beginAtZero: !0
							}
						}
					},
					elements: {
						point: {
							radius: 0,
							borderWidth: 0
						}
					},
					layout: {
						padding: {
							left: 0,
							right: 0,
							top: 5,
							bottom: 20
						}
					}
				}
			});

		}
	}
	var widgetChart2 = function(){
		if(jQuery('#widgetChart2').length > 0 ){
			const chart_widget_2 = document.getElementById("widgetChart2").getContext('2d');

			new Chart(chart_widget_2, {
				type: "line",
				data: {
					labels: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "January", "February", "March", "April"],

					datasets: [{
						label: "Sales Stats",
						backgroundColor: ['rgba(234, 73, 137, 0)'],
						borderColor: '#1EAAE7',
						pointBackgroundColor: '#1EAAE7',
						pointBorderColor: '#1EAAE7',
						borderWidth:4,
						borderRadius:10,
						pointHoverBackgroundColor: '#1EAAE7',
						pointHoverBorderColor: '#1EAAE7',
						tension:0.5,
						
						data: [12, 20, 16, 13, 16, 24, 20, 21, 19, 23, 17, 14, 24, 21, 13, 15, 27, 29, 21, 11, 14, 19, 21, 17]
					}]
				},
				options: {
					title: {
						display: !1
					},
					
					
					plugins:{
						legend:false,
						tooltip: {
							intersect: !1,
							mode: "nearest",
							xPadding: 10,
							yPadding: 10,
							caretPadding: 10
						},
						
					},
					responsive: !0,
					maintainAspectRatio: !1,
					hover: {
						mode: "index"
					},
					scales: {
						x: {
							display: !1,
							gridLines: !1,
							scaleLabel: {
								display: !0,
								labelString: "Month"
							}
						},
						y: {
							display: !1,
							gridLines: !1,
							scaleLabel: {
								display: !0,
								labelString: "Value"
							},
							ticks: {
								beginAtZero: !0
							}
						}
					},
					elements: {
						point: {
							radius: 0,
							borderWidth: 0
						}
					},
					layout: {
						padding: {
							left: 0,
							right: 0,
							top: 5,
							bottom: 20
						}
					}
				}
			});

		}
	}
	var widgetChart3 = function(){
		if(jQuery('#widgetChart3').length > 0 ){
			const widgetChart3 = document.getElementById("widgetChart3").getContext('2d');
			//generate gradient
			const widgetChart3gradientStroke = widgetChart3.createLinearGradient(0, 1, 0, 500);
			widgetChart3gradientStroke.addColorStop(0, "rgba(30, 170, 231, 0.7)");
			widgetChart3gradientStroke.addColorStop(1, "rgba(30, 170, 231, 0)");

			new Chart(widgetChart3, {
				type: "line",
				data: {
					labels: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "January", "February", "March", "April"],

					datasets: [{
						label: "Sales Stats",
						borderColor: 'transparent',
						pointBackgroundColor: '#1EAAE7',
						pointBorderColor: '#1EAAE7',
						borderWidth:4,
						borderRadius:10,
						pointHoverBackgroundColor: '#1EAAE7',
						pointHoverBorderColor: '#1EAAE7',
						backgroundColor: widgetChart3gradientStroke,
						tension:0.5,
						fill:true,
						
						data: [12, 20, 16, 13, 16, 24, 20, 21, 19, 23, 17, 14, 24, 21, 13, 15, 27, 29, 21, 11, 14, 19, 21, 17]
					}]
				},
				options: {
					title: {
						display: !1
					},
					
					
					
					plugins:{
						legend:false,
						tooltip: {
							intersect: !1,
							mode: "nearest",
							xPadding: 10,
							yPadding: 10,
							caretPadding: 10
						
						},
						
					},
					responsive: !0,
					maintainAspectRatio: !1,
					hover: {
						mode: "index"
					},
					scales: {
						x:{
							display: !1,
							gridLines: !1,
							scaleLabel: {
								display: !0,
								labelString: "Month"
							}
						},
						y:{
							display: !1,
							gridLines: !1,
							scaleLabel: {
								display: !0,
								labelString: "Value"
							},
							ticks: {
								beginAtZero: !0
							}
						}
					},
					elements: {
						point: {
							radius: 0,
							borderWidth: 0
						}
					},
					layout: {
						padding: {
							left: 0,
							right: 0,
							top: 5,
							bottom: 0
						}
					}
				}
			});

		}
	}
	var donutChart1 = function(){
		$("span.donut1").peity("donut", {
			width: "90",
			height: "90"
		});
	}
	var lineChart = function(){
		//dual line chart
		if(jQuery('#lineChart').length > 0 ){
			
			if($(window).width() < 991)
			{
			 jQuery('#lineChart').removeAttr('height');
			}
			
			const lineChart = document.getElementById("lineChart").getContext('2d');
			//generate gradient
			const lineChart_3gradientStroke1 = lineChart.createLinearGradient(500, 0, 100, 0);
			lineChart_3gradientStroke1.addColorStop(0, "rgba(26, 51, 213, 1)");
			lineChart_3gradientStroke1.addColorStop(1, "rgba(26, 51, 213, 0.5)");

			const lineChart_3gradientStroke2 = lineChart.createLinearGradient(500, 0, 100, 0);
			lineChart_3gradientStroke2.addColorStop(0, "rgba(56, 164, 248, 1)");
			lineChart_3gradientStroke2.addColorStop(1, "#ce1d76");

			/*  Chart.controllers.line = Chart.controllers.line.extend({
				draw: function () {
					draw.apply(this, arguments);
					let nk = this.chart.chart.ctx;
					let _stroke = nk.stroke;
					nk.stroke = function () {
						nk.save();
						nk.shadowColor = 'rgba(0, 0, 0, 0)';
						nk.shadowBlur = 10;
						nk.shadowOffsetX = 0;
						nk.shadowOffsetY = 10;
						_stroke.apply(this, arguments)
						nk.restore();
					}
				}
			});  */
				
			lineChart.height = 250;

			new Chart(lineChart, {
				type: 'line',
				data: {
					defaultFontFamily: 'Poppins',
					labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul"],
					datasets: [
						{
							label: "My First dataset",
							data: [40, 50, 40, 50, 40, 50, 40],
							borderColor: 'rgba(0,0,0,0)',
							borderWidth: "1",
							backgroundColor: 'rgba(39,120,238,0.8)', 
							tension : 0.3,
							fill:true,
							pointBackgroundColor: 'rgba(26, 51, 213, 0.5)'
						}, {
							label: "My First dataset",
							data: [50, 60, 50, 60, 50, 60, 50],
							borderColor: 'rgba(0,0,0,0)',
							borderWidth: "1",
							backgroundColor: 'rgba(91,171,240,0.8)',
							tension : 0.3,
							fill:true,
							pointBackgroundColor: 'rgba(56, 164, 248, 1)'
						}, {
							label: "My First dataset",
							data: [60, 70, 60, 70, 60, 70, 60],
							borderColor: 'rgba(0,0,0,0)',
							borderWidth: "1",
							backgroundColor: 'rgba(162,218,243,0.8)',
							tension : 0.3,
							fill:true,
							pointBackgroundColor: 'rgba(56, 164, 248, 1)'
						}
						
					]
				},
				options: {
					responsive:true,
					plugins:{
							legend: false,
						
					},
					 
					elements: {
							point:{
								radius: 0
							}
					},
					scales: {
						y:{
							max: 100, 
							min: 0, 
							ticks: {
							stepSize: 20, 
								beginAtZero: true, 
								padding: 10
							},
							display:false,
						},
						x: { 
							ticks: {
								padding: 5
							},
							display:false,
						}
					}
				}
			});
			

		}
		
	}
	/* Function ============ */
		return {
			init:function(){
			},
			
			
			load:function(){
				chartBar();
				donutChart1();
				widgetChart1();
				widgetChart2();
				widgetChart3();
				lineChart();
			},
			
			resize:function(){
				
			}
		}
	
	}();

	jQuery(document).ready(function(){
	});
		
	jQuery(window).on('load',function(){
		setTimeout(function(){
			dzChartlist.load();
		}, 1000); 
		
	});

	jQuery(window).on('resize',function(){
		
		
	});     

})(jQuery);