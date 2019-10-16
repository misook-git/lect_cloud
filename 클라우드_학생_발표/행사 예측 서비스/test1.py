import requests
from bs4 import BeautifulSoup as bs
import sys
import urllib.request
import os, uuid, sys
from azure.storage.blob import BlockBlobService, PublicAccess
import glob



def run_sample():
    try:
		
        # Create the BlockBlockService that is used to call the Blob service for the storage account
        block_blob_service = BlockBlobService(account_name='namstorage', account_key='cHYqLKNuFyuW0rtr75qWxSpzkjobuE4E+X7PgCrFBNTY2WdeE7TpmzRt3rgX1hZI+FpmiSYqo7wkGlyBIe+VOw==')

        # Create a container called 'quickstartblobs'.
        container_name ='quickstartblobs'
        block_blob_service.create_container(container_name)

        # Set the permission so the blobs are public.
        block_blob_service.set_container_acl(container_name, public_access=PublicAccess.Container)

        # Create a file in Documents to test the upload and download.
        local_path="C:/Users/silla/Desktop/yolov3/image/newpythonblob-master/data/"
        #local_file_name ="img0.png"
        full_path_to_file =os.path.join(local_path, local_file_name)

        # Write text to the file.
        #file = open(full_path_to_file,  'w')
        #file.write("Hello, World!")
        #file.close()

        #print("Temp file = " + full_path_to_file)
        #print("\nUploading to Blob storage as blob" + local_file_name)

        # Upload the created file, use local_file_name for the blob name
        block_blob_service.create_blob_from_path(container_name, local_file_name, full_path_to_file)

        # List the blobs in the container
        #print("\nList blobs in the container")
        generator = block_blob_service.list_blobs(container_name)
        #for blob in generator:
         #   print("\t Blob name: " + blob.name)

        # Download the blob(s).
        # Add '_DOWNLOADED' as prefix to '.txt' so you can see both files in Documents.
        #full_path_to_file2 = os.path.join(local_path, str.replace(local_file_name ,'.txt', '_DOWNLOADED.txt'))
        #print("\nDownloading blob to " + full_path_to_file2)
        #block_blob_service.get_blob_to_path(container_name, local_file_name, full_path_to_file2)

        #sys.stdout.write("Sample finished running. When you hit <any key>, the sample will be deleted and the sample "
                         #"application will exit.")
        #sys.stdout.flush()
        #input()

       # Clean up resources. This includes the container and the temp files
       # block_blob_service.delete_container(container_name)
       # os.remove(full_path_to_file)
       # os.remove(full_path_to_file2)
    except Exception as e:
        print(e)





if __name__=="__main__" :

	sys.stdout = open('data/output.txt','w')
	url="https://www.halinbbal.com/sale/index/popular/1?cate_all=1"
	r = requests.get(url)

	data = r.text

	soup = bs(data, 'html.parser')

	clist = soup.select("div > h3")
	for s in clist:
		print(s.get_text())
	sys.stdout.close()
	
	
	url = "https://www.google.com/search?q=%EC%84%B8%EC%9D%BC&tbm=isch&source=iu&ictx=1&fir=_2rGcMBhClfyLM%253A%252CGoHe3WUiknEyDM%252C_&vet=1&usg=AI4_-kS1rAlZXZ1WwNWn1KHFI9Lbyfd4-Q&sa=X&ved=2ahUKEwiEu7Xdx-DiAhUDOrwKHYdwCDkQ9QEwCXoECAUQFg#imgrc=_&vet=1"

	opener = urllib.request.build_opener()
	opener.add_headers = [{'User-Agent' : 'Mozilla'}]
	urllib.request.install_opener(opener)

	raw = requests.get(url).text
	soup = bs(raw, 'html.parser')

	imgs = soup.find_all('img')

	links = []

	for img in imgs:
		link = img.get('src')
		if 'http://' not in link:
			link = url + link
		links.append(link)

	for i in range(len(links)):
		filename = 'data/img{}.png'.format(i)
		urllib.request.urlretrieve(links[i], filename)

	
images = glob.glob('C:/Users/silla/Desktop/yolov3/image/newpythonblob-master/data/*.png')
texts = glob.glob('C:/Users/silla/Desktop/yolov3/image/newpythonblob-master/data/*.txt')
for imgname in images:
	local_file_name = os.path.basename(imgname)
	run_sample()
for txtname in texts:
	local_file_name = os.path.basename(txtname)
	run_sample()
