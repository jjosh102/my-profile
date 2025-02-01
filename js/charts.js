function getChartContext(canvasId) {
  const canvas = document.getElementById(canvasId);
  return canvas.getContext('2d');
}

function renderRepoStatsChart(ctx, stargazers, forks, watchers) {
  new Chart(ctx, {
    type: 'bar',
    data: {
      labels: ['Stargazers', 'Forks', 'Watchers'],
      datasets: [{
        label: 'Count',
        data: [stargazers, forks, watchers],
        backgroundColor: [
          'rgba(88, 166, 255, 0.2)',
          'rgba(240, 118, 19, 0.2)',  
          'rgba(163, 113, 247, 0.2)' 
        ],
        borderColor: [
          'rgba(88, 166, 255, 1)',   
          'rgba(240, 118, 19, 1)',   
          'rgba(163, 113, 247, 1)'    
        ],
        borderWidth: 1
      }]
    },
    options: {
      scales: {
        y: {
          beginAtZero: true
        }
      }
    }
  });
}