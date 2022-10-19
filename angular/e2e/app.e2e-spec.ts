import { ProcessManagementTemplatePage } from './app.po';

describe('ProcessManagement App', function() {
  let page: ProcessManagementTemplatePage;

  beforeEach(() => {
    page = new ProcessManagementTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
