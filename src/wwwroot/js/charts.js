function renderCodeFrequencyChart(weeks, additions, deletions) {
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
                  borderWidth: 1
              },
              {
                  label: 'Deletions',
                  data: deletions,
                  backgroundColor: 'rgba(248, 81, 73, 0.2)', 
                  borderColor: 'rgba(248, 81, 73, 1)',
                  borderWidth: 1
              }
          ]
      },
      options: {
          scales: {
              x: {
                  stacked: true
              },
              y: {
                  stacked: true,
                  beginAtZero: true,
                  ticks: {
                      callback: function (value) {
                          return Math.abs(value); 
                      }
                  }
              }
          },
          plugins: {
              tooltip: {
                  callbacks: {
                      label: function (context) {
                          let label = context.dataset.label || '';
                          if (label) {
                              label += ': ';
                          }
                          if (context.parsed.y !== null) {
                              label += Math.abs(context.parsed.y); 
                          }
                          return label;
                      }
                  }
              }
          }
      }
  });
}