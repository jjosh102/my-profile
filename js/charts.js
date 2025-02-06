
window.renderCodeFrequencyChart = function (weeks, additions, deletions) {
    const ctx = document.getElementById('codeFrequencyChart').getContext('2d');
    new Chart(ctx, {
        type: 'bar',
        data: {
            labels: weeks,
            datasets: [
                {
                    label: 'Additions',
                    data: additions,
                    backgroundColor: 'rgba(46, 160, 67, 0.2)',
                    borderColor: 'rgba(46, 160, 67, 1)',
                    borderWidth: 1,
                },
                {
                    label: 'Deletions',
                    data: deletions,
                    backgroundColor: 'rgba(248, 81, 73, 0.2)',
                    borderColor: 'rgba(248, 81, 73, 1)',
                    borderWidth: 1,
                },
            ],
        },
        options: {
            scales: {
                x: { stacked: true },
                y: {
                    stacked: true,
                    beginAtZero: true,
                    ticks: {
                        callback: (value) => Math.abs(value),
                    },
                },
            },
            plugins: {
                tooltip: {
                    callbacks: {
                        label: (context) => {
                            let label = context.dataset.label || '';
                            if (label) label += ': ';
                            if (context.parsed.y !== null) {
                                label += Math.abs(context.parsed.y);
                            }
                            return label;
                        },
                    },
                },
            },
        },
    });
};


window.renderCodeFrequencyChart = function (weeks, additions, deletions) {
    const ctx = document.getElementById('codeFrequencyChart').getContext('2d');
    new Chart(ctx, {
        type: 'bar',
        data: {
            labels: weeks,
            datasets: [
                {
                    label: 'Additions',
                    data: additions,
                    backgroundColor: 'rgba(46, 160, 67, 0.2)',
                    borderColor: 'rgba(46, 160, 67, 1)',
                    borderWidth: 1,
                },
                {
                    label: 'Deletions',
                    data: deletions,
                    backgroundColor: 'rgba(248, 81, 73, 0.2)',
                    borderColor: 'rgba(248, 81, 73, 1)',
                    borderWidth: 1,
                },
            ],
        },
        options: {
            scales: {
                x: { stacked: true },
                y: {
                    stacked: true,
                    beginAtZero: true,
                    ticks: {
                        callback: (value) => Math.abs(value),
                    },
                },
            },
            plugins: {
                tooltip: {
                    callbacks: {
                        label: (context) => {
                            let label = context.dataset.label || '';
                            if (label) label += ': ';
                            if (context.parsed.y !== null) {
                                label += Math.abs(context.parsed.y);
                            }
                            return label;
                        },
                    },
                },
            },
        },
    });
};

window.renderContributionHeatmap = (dates, counts) => {
    if (typeof ChartMatrix === 'undefined' && typeof Chart !== 'undefined') {
        Chart.register(
            window['chartjs-chart-matrix'].MatrixController,
            window['chartjs-chart-matrix'].MatrixElement
        );
    }

    const canvas = document.getElementById('contributionHeatmap');
    const ctx = canvas.getContext('2d');

    const startDate = new Date(dates[0]);
    const endDate = new Date(dates[dates.length - 1]);
    const totalWeeks = Math.ceil((endDate - startDate) / (1000 * 60 * 60 * 24 * 7));
    const maxCount = Math.max(...counts);

    const colors = ['#161b22', '#0e4429', '#006d32', '#26a641', '#39d353'];
    const getColor = value => {
        if (value === 0) return colors[0];
        if (value <= maxCount * 0.25) return colors[1];
        if (value <= maxCount * 0.50) return colors[2];
        if (value <= maxCount * 0.75) return colors[3];
        return colors[4];
    };

    const data = {
        datasets: [{
            label: 'Contributions',
            data: dates.map((date, index) => {
                const currentDate = new Date(date);
                const diffDays = Math.floor(
                    (currentDate - startDate) / (1000 * 60 * 60 * 24)
                );
                const startDayOfWeek = startDate.getDay();
                return {
                    x: Math.floor((diffDays + startDayOfWeek) / 7),
                    y: currentDate.getDay(),
                    d: date,
                    v: counts[index]
                };
            }),
            backgroundColor: ctx => getColor(ctx.raw.v),
            borderRadius: 2,
            width: c => {
                const chartArea = c.chart.chartArea;
                if (!chartArea) return 0;
                return (chartArea.right - chartArea.left) / totalWeeks - 3;
            },
            height: c => {
                const chartArea = c.chart.chartArea;
                if (!chartArea) return 0;
                return (chartArea.bottom - chartArea.top) / 7 - 3;
            },
            borderWidth: 0,
        }]
    };

    const options = {
        plugins: {
            legend: {
                display: true,
                position: 'bottom',
                labels: {
                    color: '#8b949e',
                    generateLabels: chart => [
                        {
                            text: 'Less',
                            fillStyle: 'transparent',
                            strokeStyle: 'transparent',
                            fontColor: '#8b949e',
                            boxWidth: 0,
                            borderWidth: 0
                        },
                        ...colors.map(color => ({
                            text: '',
                            fillStyle: color,
                            strokeStyle: '#0d1117',
                            lineWidth: 2,
                            borderRadius: 2
                        })),
                        {
                            text: 'More',
                            fillStyle: 'transparent',
                            strokeStyle: 'transparent',
                            fontColor: '#8b949e',
                            boxWidth: 0,
                            borderWidth: 0
                        }
                    ],
                    boxWidth: 10,
                    padding: 12
                }
            },
            tooltip: {
                displayColors: false,
                backgroundColor: '#161b22',
                titleColor: '#c9d1d9',
                bodyColor: '#c9d1d9',
                callbacks: {
                    title() {
                        return '';
                    },
                    label(context) {
                        const { d, v } = context.dataset.data[context.dataIndex];
                        return `${v} contributions on ${new Date(d).toLocaleDateString('en-US', {
                            month: 'long',
                            day: 'numeric'
                        })}`;
                    }
                }
            }
        },
        scales: {
            x: {
                position: 'top',
                display: true,
                offset: false,
                min: -0.5,
                max: totalWeeks - 0.5,
                grid: {
                    display: false,
                    drawBorder: false,
                },
                ticks: {
                    color: '#7D8590',
                    maxRotation: 0,
                    autoSkip: false,
                    padding: 8,
                    font: {
                        size: 11,
                    },
                    callback: function (value) {
                        const date = new Date(startDate);
                        date.setDate(date.getDate() + (value * 7));
                        return date.toLocaleString('en-US', {
                            month: 'short'
                        });
                    }
                },
                border: {
                    display: false
                }
            },
            y: {
                display: true,
                offset: false,
                min: -0.5,
                max: 6.5,
                position: 'left',
                grid: {
                    display: false,
                    drawBorder: false,
                },
                ticks: {
                    color: '#7D8590',
                    padding: 8,
                    font: {
                        size: 11,
                    },
                    callback: function (value) {
                        const days = ['Mon', '', 'Wed', '', 'Fri', '', ''];
                        return days[value];
                    }
                },
                border: {
                    display: false
                }
            }
        },
        responsive: true,
        layout: {
            padding: { top: 12, right: 2, bottom: 2, left: 2 }
        }
    };

    const config = { type: 'matrix', data: data, options: options };
    new Chart(ctx, config);
};
